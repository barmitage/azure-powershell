#
# Module manifest for module 'PSGet_AzureRM.Automation'
#
# Generated by: Microsoft Corporation
#
# Generated on: 3/20/2018
#

@{

# Script module or binary module file associated with this manifest.
# RootModule = ''

# Version number of this module.
ModuleVersion = '4.3.1'

# Supported PSEditions
# CompatiblePSEditions = @()

# ID used to uniquely identify this module
GUID = 'bcea1c70-a32b-48c3-a05c-323e1c02f4d3'

# Author of this module
Author = 'Microsoft Corporation'

# Company or vendor of this module
CompanyName = 'Microsoft Corporation'

# Copyright statement for this module
Copyright = 'Microsoft Corporation. All rights reserved.'

# Description of the functionality provided by this module
Description = 'Microsoft Azure PowerShell - Automation service cmdlets for Azure Resource Manager'

# Minimum version of the Windows PowerShell engine required by this module
PowerShellVersion = '3.0'

# Name of the Windows PowerShell host required by this module
# PowerShellHostName = ''

# Minimum version of the Windows PowerShell host required by this module
# PowerShellHostVersion = ''

# Minimum version of Microsoft .NET Framework required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
DotNetFrameworkVersion = '4.5.2'

# Minimum version of the common language runtime (CLR) required by this module. This prerequisite is valid for the PowerShell Desktop edition only.
CLRVersion = '4.0'

# Processor architecture (None, X86, Amd64) required by this module
# ProcessorArchitecture = ''

# Modules that must be imported into the global environment prior to importing this module
RequiredModules = @(@{ModuleName = 'AzureRM.Profile'; ModuleVersion = '4.5.0'; })

# Assemblies that must be loaded prior to importing this module
RequiredAssemblies = '.\Microsoft.Azure.Management.Automation.dll'

# Script files (.ps1) that are run in the caller's environment prior to importing this module.
# ScriptsToProcess = @()

# Type files (.ps1xml) to be loaded when importing this module
# TypesToProcess = @()

# Format files (.ps1xml) to be loaded when importing this module
# FormatsToProcess = @()

# Modules to import as nested modules of the module specified in RootModule/ModuleToProcess
NestedModules = @('.\Microsoft.Azure.Commands.ResourceManager.Automation.dll')

# Functions to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no functions to export.
FunctionsToExport = @()

# Cmdlets to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no cmdlets to export.
CmdletsToExport = 'Get-AzureRMAutomationHybridWorkerGroup', 
               'Get-AzureRmAutomationJobOutputRecord', 
               'Import-AzureRmAutomationDscNodeConfiguration', 
               'Export-AzureRmAutomationDscConfiguration', 
               'Export-AzureRmAutomationDscNodeReportContent', 
               'Get-AzureRmAutomationCertificate', 
               'Get-AzureRmAutomationConnection', 
               'Get-AzureRmAutomationCredential', 
               'Get-AzureRmAutomationDscCompilationJob', 
               'Get-AzureRmAutomationDscCompilationJobOutput', 
               'Get-AzureRmAutomationDscNodeConfiguration', 
               'Get-AzureRmAutomationDscNodeReport', 'Get-AzureRmAutomationJob', 
               'Get-AzureRmAutomationJobOutput', 'Get-AzureRmAutomationModule', 
               'Get-AzureRmAutomationRunbook', 'Export-AzureRmAutomationRunbook', 
               'Get-AzureRmAutomationSchedule', 
               'Get-AzureRmAutomationScheduledRunbook', 
               'Get-AzureRmAutomationVariable', 'Get-AzureRmAutomationWebhook', 
               'New-AzureRmAutomationCertificate', 
               'New-AzureRmAutomationConnection', 
               'New-AzureRmAutomationCredential', 'New-AzureRmAutomationModule', 
               'New-AzureRmAutomationRunbook', 'New-AzureRmAutomationSchedule', 
               'New-AzureRmAutomationVariable', 'New-AzureRmAutomationWebhook', 
               'Publish-AzureRmAutomationRunbook', 
               'Register-AzureRmAutomationDscNode', 
               'Register-AzureRmAutomationScheduledRunbook', 
               'Remove-AzureRmAutomationCertificate', 
               'Remove-AzureRmAutomationConnectionType', 
               'Remove-AzureRmAutomationConnection', 
               'Remove-AzureRmAutomationCredential', 
               'Remove-AzureRmAutomationDscNodeConfiguration', 
               'Remove-AzureRmAutomationModule', 'Remove-AzureRmAutomationRunbook', 
               'Remove-AzureRmAutomationSchedule', 
               'Remove-AzureRmAutomationVariable', 
               'Remove-AzureRmAutomationDscConfiguration', 
               'Remove-AzureRmAutomationWebhook', 'Resume-AzureRmAutomationJob', 
               'Set-AzureRmAutomationCertificate', 
               'Set-AzureRmAutomationConnectionFieldValue', 
               'Set-AzureRmAutomationCredential', 'Set-AzureRmAutomationModule', 
               'Set-AzureRmAutomationRunbook', 'Import-AzureRmAutomationRunbook', 
               'Set-AzureRmAutomationSchedule', 'Set-AzureRmAutomationVariable', 
               'Set-AzureRmAutomationWebhook', 
               'Start-AzureRmAutomationDscCompilationJob', 
               'Get-AzureRmAutomationRegistrationInfo', 
               'Get-AzureRmAutomationDscConfiguration', 
               'Get-AzureRmAutomationDscNode', 
               'Get-AzureRmAutomationDscOnboardingMetaconfig', 
               'Import-AzureRmAutomationDscConfiguration', 
               'New-AzureRmAutomationKey', 'Start-AzureRmAutomationRunbook', 
               'Stop-AzureRmAutomationJob', 'Suspend-AzureRmAutomationJob', 
               'Unregister-AzureRmAutomationDscNode', 
               'Set-AzureRmAutomationAccount', 'Remove-AzureRmAutomationAccount', 
               'New-AzureRmAutomationAccount', 'Get-AzureRmAutomationAccount', 
               'Set-AzureRmAutomationDscNode', 
               'Unregister-AzureRmAutomationScheduledRunbook', 
               'Start-AzureRmAutomationDscNodeConfigurationDeployment', 
               'Stop-AzureRmAutomationDscNodeConfigurationDeployment', 
               'Get-AzureRmAutomationDscNodeConfigurationDeploymentSchedule', 
               'Get-AzureRmAutomationDscNodeConfigurationDeployment'

# Variables to export from this module
# VariablesToExport = @()

# Aliases to export from this module, for best performance, do not use wildcards and do not delete the entry, use an empty array if there are no aliases to export.
AliasesToExport = 'Import-AzureRmAutomationModule'

# DSC resources to export from this module
# DscResourcesToExport = @()

# List of all modules packaged with this module
# ModuleList = @()

# List of all files packaged with this module
# FileList = @()

# Private data to pass to the module specified in RootModule/ModuleToProcess. This may also contain a PSData hashtable with additional module metadata used by PowerShell.
PrivateData = @{

    PSData = @{

        # Tags applied to this module. These help with module discovery in online galleries.
        Tags = 'Azure','ResourceManager','ARM','Automation'

        # A URL to the license for this module.
        LicenseUri = 'https://aka.ms/azps-license'

        # A URL to the main website for this project.
        ProjectUri = 'https://github.com/Azure/azure-powershell'

        # A URL to an icon representing this module.
        # IconUri = ''

        # ReleaseNotes of this module
        ReleaseNotes = '* Fix issue with Default Resource Group in CloudShell'

        # Prerelease string of this module
        # Prerelease = ''

        # Flag to indicate whether the module requires explicit user acceptance for install/update
        # RequireLicenseAcceptance = $false

        # External dependent modules of this module
        # ExternalModuleDependencies = @()

    } # End of PSData hashtable
    
 } # End of PrivateData hashtable

# HelpInfo URI of this module
# HelpInfoURI = ''

# Default prefix for commands exported from this module. Override the default prefix using Import-Module -Prefix.
# DefaultCommandPrefix = ''

}

