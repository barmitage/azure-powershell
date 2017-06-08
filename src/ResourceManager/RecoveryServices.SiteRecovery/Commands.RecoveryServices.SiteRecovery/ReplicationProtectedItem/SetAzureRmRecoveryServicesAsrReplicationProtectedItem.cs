// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models;

namespace Microsoft.Azure.Commands.RecoveryServices.SiteRecovery
{
    /// <summary>
    /// Retrieves Azure Site Recovery Protection Entity.
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "AzureRmRecoveryServicesAsrReplicationProtectedItem", DefaultParameterSetName = ASRParameterSets.ByObject)]
    [Alias("Set-ASRReplicationProtectedItem")]
    [OutputType(typeof(ASRJob))]
    public class SetAzureRmRecoveryServicesAsrReplicationProtectedItem : SiteRecoveryCmdletBase
    {
        #region Parameters

        /// <summary>
        /// Gets or sets ID of the Virtual Machine.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [ValidateNotNullOrEmpty]
        public ASRReplicationProtectedItem ReplicationProtectedItem { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure VM given name
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure VM size
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets Selected Primary Network interface card Id
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string PrimaryNic { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Network Id
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string RecoveryNetworkId { get; set; }

        /// <summary>
        /// Gets or sets Recovery Azure Network Id
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string RecoveryNicSubnetName { get; set; }

        /// <summary>
        /// Gets or sets Recovery Nic Static IPAddress
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string RecoveryNicStaticIPAddress { get; set; }

        /// <summary>
        /// Gets or sets Selection Type for Nic
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.NotSelected,
            Constants.SelectedByUser)]
        public string NicSelectionType { get; set; }

        /// <summary>
        /// Gets or sets Recovery Resource ID
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        public string RecoveryResourceGroupId { get; set; }

        /// <summary>
        /// Gets or sets LicenseType for
        /// HUB https://azure.microsoft.com/en-in/pricing/hybrid-use-benefit/
        /// </summary>
        [Parameter]
        [ValidateNotNullOrEmpty]
        [ValidateSet(
            Constants.NoLicenseType,
            Constants.LicenseTypeWindowsServer)]
        public string LicenseType { get; set; }

        #endregion Parameters

        /// <summary>
        /// ProcessRecord of the command.
        /// </summary>
        public override void ExecuteSiteRecoveryCmdlet()
        {
            base.ExecuteSiteRecoveryCmdlet();

            var replicationProtectedItemResponse =
                RecoveryServicesClient.GetAzureSiteRecoveryReplicationProtectedItem(Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics),
                Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers), 
                ReplicationProtectedItem.Name);

            var provider =
                replicationProtectedItemResponse.Properties.ProviderSpecificDetails;

            // Check for Replication Provider type HyperVReplicaAzure/InMageAzureV2
            if (!(provider is HyperVReplicaAzureReplicationDetails) &&
                !(provider is InMageAzureV2ReplicationDetails))
            {
                this.WriteWarning(Properties.Resources.UnsupportedReplicationProvidedForUpdateVmProperties.ToString());
                return;
            }

            // Check for at least one option
            if (string.IsNullOrEmpty(this.Name) &&
                string.IsNullOrEmpty(this.Size) &&
                string.IsNullOrEmpty(this.PrimaryNic) &&
                string.IsNullOrEmpty(this.RecoveryNetworkId) &&
                string.IsNullOrEmpty(this.RecoveryResourceGroupId) &&
                string.IsNullOrEmpty(this.LicenseType))
            {
                this.WriteWarning(Properties.Resources.ArgumentsMissingForUpdateVmProperties.ToString());
                return;
            }

            // Both primary & recovery inputs should be present
            if (string.IsNullOrEmpty(this.PrimaryNic) ^
                string.IsNullOrEmpty(this.RecoveryNetworkId))
            {
                this.WriteWarning(Properties.Resources.NetworkArgumentsMissingForUpdateVmProperties.ToString());
                return;
            }

            string vmName = this.Name;
            string vmSize = this.Size;
            string vmRecoveryNetworkId = this.RecoveryNetworkId;
            string licenseType = this.LicenseType;
            string recoveryResourceGroupId = this.RecoveryResourceGroupId;
            var vMNicInputDetailsList = new List<VMNicInputDetails>();
            VMNicDetails vMNicDetailsToBeUpdated;
            var providerSpecificInput = new UpdateReplicationProtectedItemProviderInput();

            if (provider is HyperVReplicaAzureReplicationDetails)
            {
                var providerSpecificDetails =
                    (HyperVReplicaAzureReplicationDetails)replicationProtectedItemResponse.Properties.ProviderSpecificDetails;
            
                if (string.IsNullOrEmpty(this.RecoveryResourceGroupId))
                {
                    recoveryResourceGroupId = providerSpecificDetails.RecoveryAzureResourceGroupId;
                }

                string deploymentType = Utilities.GetValueFromArmId(providerSpecificDetails.RecoveryAzureStorageAccount, ARMResourceTypeConstants.Providers);
                if (deploymentType.ToLower().Contains(Constants.Classic.ToLower()))
                {
                    providerSpecificInput = new HyperVReplicaAzureUpdateReplicationProtectedItemInput()
                    {
                        RecoveryAzureV1ResourceGroupId = recoveryResourceGroupId,
                        RecoveryAzureV2ResourceGroupId = null
                    };
                }
                else
                {
                    providerSpecificInput = new HyperVReplicaAzureUpdateReplicationProtectedItemInput()
                    {
                        RecoveryAzureV1ResourceGroupId = null,
                        RecoveryAzureV2ResourceGroupId = recoveryResourceGroupId
                    };
                }

                if (string.IsNullOrEmpty(this.Name))
                {
                    vmName = providerSpecificDetails.RecoveryAzureVMName;
                }

                if (string.IsNullOrEmpty(this.Size))
                {
                    vmSize = providerSpecificDetails.RecoveryAzureVMSize;
                }

                if (string.IsNullOrEmpty(this.RecoveryNetworkId))
                {
                    vmRecoveryNetworkId = providerSpecificDetails.SelectedRecoveryAzureNetworkId;
                }

                if (string.IsNullOrEmpty(this.LicenseType))
                {
                    //licenseType = providerSpecificDetails.LicenseType;
                }

                if (!string.IsNullOrEmpty(this.PrimaryNic))
                {
                    if (providerSpecificDetails.VmNics != null)
                    {
                        vMNicDetailsToBeUpdated = providerSpecificDetails.VmNics.SingleOrDefault(
                            n => string.Compare(n.NicId, this.PrimaryNic, StringComparison.OrdinalIgnoreCase) == 0);
                        if (vMNicDetailsToBeUpdated != null)
                        {
                            VMNicInputDetails vMNicInputDetails = new VMNicInputDetails();

                            vMNicInputDetails.NicId = this.PrimaryNic;
                            vMNicInputDetails.RecoveryVMSubnetName = this.RecoveryNicSubnetName;
                            vMNicInputDetails.ReplicaNicStaticIPAddress = this.RecoveryNicStaticIPAddress;
                            vMNicInputDetails.SelectionType =
                                string.IsNullOrEmpty(this.NicSelectionType) ? Constants.SelectedByUser : this.NicSelectionType;
                            vMNicInputDetailsList.Add(vMNicInputDetails);

                            IEnumerable<VMNicDetails> vMNicDetailsListRemaining = providerSpecificDetails.VmNics.Where(
                                n => string.Compare(n.NicId, this.PrimaryNic, StringComparison.OrdinalIgnoreCase) != 0);
                            foreach (VMNicDetails nDetails in vMNicDetailsListRemaining)
                            {
                                vMNicInputDetails = new VMNicInputDetails();

                                vMNicInputDetails.NicId = nDetails.NicId;
                                vMNicInputDetails.RecoveryVMSubnetName = nDetails.RecoveryVMSubnetName;
                                vMNicInputDetails.ReplicaNicStaticIPAddress = nDetails.ReplicaNicStaticIPAddress;
                                vMNicInputDetails.SelectionType = nDetails.SelectionType;
                                vMNicInputDetailsList.Add(vMNicInputDetails);
                            }
                        }
                        else
                        {
                            throw new PSInvalidOperationException(Properties.Resources.NicNotFoundInVMForUpdateVmProperties);
                        }
                    }
                }
                else
                {
                    VMNicInputDetails vMNicInputDetails;
                    foreach (VMNicDetails nDetails in providerSpecificDetails.VmNics)
                    {
                        vMNicInputDetails = new VMNicInputDetails();

                        vMNicInputDetails.NicId = nDetails.NicId;
                        vMNicInputDetails.RecoveryVMSubnetName = nDetails.RecoveryVMSubnetName;
                        vMNicInputDetails.ReplicaNicStaticIPAddress = nDetails.ReplicaNicStaticIPAddress;
                        vMNicInputDetails.SelectionType = nDetails.SelectionType;
                        vMNicInputDetailsList.Add(vMNicInputDetails);
                    }
                }
            }
            else if (provider is InMageAzureV2ReplicationDetails)
            {
                var providerSpecificDetails =
                    (InMageAzureV2ReplicationDetails)replicationProtectedItemResponse.Properties.ProviderSpecificDetails;

                if (string.IsNullOrEmpty(this.RecoveryResourceGroupId))
                {
                    recoveryResourceGroupId = providerSpecificDetails.RecoveryAzureResourceGroupId;
                }

                string deploymentType = Utilities.GetValueFromArmId(providerSpecificDetails.RecoveryAzureStorageAccount, ARMResourceTypeConstants.Providers);
                if (deploymentType.ToLower().Contains(Constants.Classic.ToLower()))
                {
                    providerSpecificInput = new InMageAzureV2UpdateReplicationProtectedItemInput()
                    {
                        RecoveryAzureV1ResourceGroupId = recoveryResourceGroupId,
                        RecoveryAzureV2ResourceGroupId = null
                    };
                }
                else
                {
                    providerSpecificInput = new InMageAzureV2UpdateReplicationProtectedItemInput()
                    {
                        RecoveryAzureV1ResourceGroupId = null,
                        RecoveryAzureV2ResourceGroupId = recoveryResourceGroupId
                    };
                }

                if (string.IsNullOrEmpty(this.Name))
                {
                    vmName = providerSpecificDetails.RecoveryAzureVMName;
                }

                if (string.IsNullOrEmpty(this.Size))
                {
                    vmSize = providerSpecificDetails.RecoveryAzureVMSize;
                }

                if (string.IsNullOrEmpty(this.RecoveryNetworkId))
                {
                    vmRecoveryNetworkId = providerSpecificDetails.SelectedRecoveryAzureNetworkId;
                }

                if (string.IsNullOrEmpty(this.LicenseType))
                {
                    //licenseType = providerSpecificDetails.LicenseType;
                }

                if (!string.IsNullOrEmpty(this.PrimaryNic))
                {
                    if (providerSpecificDetails.VmNics != null)
                    {
                        vMNicDetailsToBeUpdated = providerSpecificDetails.VmNics.SingleOrDefault(
                            n => string.Compare(n.NicId, this.PrimaryNic, StringComparison.OrdinalIgnoreCase) == 0);
                        if (vMNicDetailsToBeUpdated != null)
                        {
                            VMNicInputDetails vMNicInputDetails = new VMNicInputDetails();

                            vMNicInputDetails.NicId = this.PrimaryNic;
                            vMNicInputDetails.RecoveryVMSubnetName = this.RecoveryNicSubnetName;
                            vMNicInputDetails.ReplicaNicStaticIPAddress = this.RecoveryNicStaticIPAddress;
                            vMNicInputDetails.SelectionType = string.IsNullOrEmpty(this.NicSelectionType) ? Constants.SelectedByUser : this.NicSelectionType;
                            vMNicInputDetailsList.Add(vMNicInputDetails);

                            IEnumerable<VMNicDetails> vMNicDetailsListRemaining = providerSpecificDetails.VmNics.Where(
                                n => string.Compare(n.NicId, this.PrimaryNic, StringComparison.OrdinalIgnoreCase) != 0);
                            foreach (VMNicDetails nDetails in vMNicDetailsListRemaining)
                            {
                                vMNicInputDetails = new VMNicInputDetails();

                                vMNicInputDetails.NicId = nDetails.NicId;
                                vMNicInputDetails.RecoveryVMSubnetName = nDetails.RecoveryVMSubnetName;
                                vMNicInputDetails.ReplicaNicStaticIPAddress = nDetails.ReplicaNicStaticIPAddress;
                                vMNicInputDetails.SelectionType = nDetails.SelectionType;
                                vMNicInputDetailsList.Add(vMNicInputDetails);
                            }
                        }
                        else
                        {
                            throw new PSInvalidOperationException(Properties.Resources.NicNotFoundInVMForUpdateVmProperties);
                        }
                    }
                }
                else
                {
                    VMNicInputDetails vMNicInputDetails;
                    foreach (VMNicDetails nDetails in providerSpecificDetails.VmNics)
                    {
                        vMNicInputDetails = new VMNicInputDetails();

                        vMNicInputDetails.NicId = nDetails.NicId;
                        vMNicInputDetails.RecoveryVMSubnetName = nDetails.RecoveryVMSubnetName;
                        vMNicInputDetails.ReplicaNicStaticIPAddress = nDetails.ReplicaNicStaticIPAddress;
                        vMNicInputDetails.SelectionType = nDetails.SelectionType;
                        vMNicInputDetailsList.Add(vMNicInputDetails);
                    }
                }
            }

            var updateReplicationProtectedItemInputProperties =
                new UpdateReplicationProtectedItemInputProperties()
                {
                    RecoveryAzureVMName = vmName,
                    RecoveryAzureVMSize = vmSize,
                    SelectedRecoveryAzureNetworkId = vmRecoveryNetworkId,
                    VmNics = vMNicInputDetailsList,
                    LicenseType = licenseType == Microsoft.Azure.Management.RecoveryServices.SiteRecovery.Models.LicenseType.NoLicenseType.ToString() ? Management.RecoveryServices.SiteRecovery.Models.LicenseType.NoLicenseType : Management.RecoveryServices.SiteRecovery.Models.LicenseType.WindowsServer,
                    ProviderSpecificDetails = providerSpecificInput
                };

            var input = new UpdateReplicationProtectedItemInput()
            {
                Properties = updateReplicationProtectedItemInputProperties
            };

            var response = RecoveryServicesClient.UpdateVmProperties(
                Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationFabrics),
                Utilities.GetValueFromArmId(this.ReplicationProtectedItem.ID, ARMResourceTypeConstants.ReplicationProtectionContainers),
                ReplicationProtectedItem.Name,
                input);

            var jobResponse =
                RecoveryServicesClient
                .GetAzureSiteRecoveryJobDetails(PSRecoveryServicesClient.GetJobIdFromReponseLocation(response.Location));

            WriteObject(new ASRJob(jobResponse));
        }
    }
}
