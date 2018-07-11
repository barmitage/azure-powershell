﻿// ----------------------------------------------------------------------------------
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

using Microsoft.Azure.Commands.ResourceManager.Common.ArgumentCompleters;
using Microsoft.Azure.Management.Search.Models;
using System.Management.Automation;

namespace Microsoft.Azure.Commands.Management.Search.SearchService
{

    [Cmdlet(VerbsCommon.New, SearchServiceNounStr, SupportsShouldProcess = true), OutputType(typeof(Models.PSSearchService))]
    public class NewSearchServiceCommand : SearchServiceBaseCmdlet
    {
        [Parameter(
            Position = 0,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = ResourceGroupHelpMessage)]
        [ValidateNotNullOrEmpty]
        [ResourceGroupCompleter()]
        public string ResourceGroupName { get; set; }

        [Parameter(
            Position = 1,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = ResourceNameHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Name { get; set; }

        [Parameter(
            Position = 2,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = SkuHelpMessage)]
        public SkuName Sku { get; set; }

        [Parameter(
            Position = 3,
            Mandatory = true,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = LocationHelpMessage)]
        [ValidateNotNullOrEmpty]
        public string Location { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = PartitionCountHelpMessage)]
        public int? PartitionCount { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = ReplicaCountHelpMessage)]
        public int? ReplicaCount { get; set; }

        [Parameter(
            Mandatory = false,
            ValueFromPipelineByPropertyName = true,
            HelpMessage = HostingModeHelpMessage)]
        public HostingMode? HostingMode { get; set; }

        public override void ExecuteCmdlet()
        {
            base.ExecuteCmdlet();

            var searchService = new Azure.Management.Search.Models.SearchService(name: Name,
                                                                                location: Location,
                                                                                sku: new Sku(Sku),
                                                                                replicaCount: ReplicaCount,
                                                                                partitionCount: PartitionCount,
                                                                                hostingMode: HostingMode);

            var response = SearchClient.Services.CreateOrUpdateWithHttpMessagesAsync(ResourceGroupName, Name, searchService).Result;
            WriteSearchService(response.Body);
        }
    }
}
