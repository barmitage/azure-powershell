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

namespace Microsoft.Azure.Commands.LogicApp.Test.ScenarioTests
{
    using Microsoft.WindowsAzure.Commands.ScenarioTest;
    using Microsoft.WindowsAzure.Commands.Test.Utilities.Common;
    using ServiceManagement.Common.Models;
    using Xunit;

    /// <summary>
    /// Scenario tests for integration account map commands.
    /// </summary>
    public class IntegrationAccountMapTests : RMTestBase
    {
        public XunitTracingInterceptor _logger;

        public IntegrationAccountMapTests(Xunit.Abstractions.ITestOutputHelper output)
        {
            _logger = new XunitTracingInterceptor(output);
            XunitTracingInterceptor.AddToContext(_logger);
        }

        /// <summary>
        /// Test New-AzIntegrationAccountMap command to create a new integration account map.
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestCreateMap()
        {
            WorkflowController.NewInstance.RunPowerShellTest(_logger, "Test-CreateIntegrationAccountMap");
        }

        /// <summary>
        /// Test Get-AzIntegrationAccountMap command to get the integration account map.
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestGetMap()
        {
            WorkflowController.NewInstance.RunPowerShellTest(_logger, "Test-GetIntegrationAccountMap");
        }

        /// <summary>
        /// Test Remove-AzIntegrationAccountMap command to remove the integration account map.
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestRemoveMap()
        {
            WorkflowController.NewInstance.RunPowerShellTest(_logger, "Test-RemoveIntegrationAccountMap");
        }

        /// <summary>
        /// Test Set-AzIntegrationAccountMap command to update the integration account map.
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestUpdateMap()
        {
            WorkflowController.NewInstance.RunPowerShellTest(_logger, "Test-UpdateIntegrationAccountMap");
        }

        /// <summary>
        /// Test Get-AzIntegrationAccountMap command to get all the integration account map.
        /// </summary>
        [Fact]
        [Trait(Category.AcceptanceType, Category.CheckIn)]
        public void TestListMap()
        {
            WorkflowController.NewInstance.RunPowerShellTest(_logger, "Test-ListIntegrationAccountMap");
        }
    }
}