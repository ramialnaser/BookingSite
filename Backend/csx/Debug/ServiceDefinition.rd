<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="Backend" generation="1" functional="0" release="0" Id="076a96b2-66b2-4b46-90e4-009319e69010" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="BackendGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="FRSApi:FRSEndPoint" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Backend/BackendGroup/LB:FRSApi:FRSEndPoint" />
          </inToChannel>
        </inPort>
        <inPort name="PSApi:PSEndPoint" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/Backend/BackendGroup/LB:PSApi:PSEndPoint" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="FRSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Backend/BackendGroup/MapFRSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="FRSApiInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Backend/BackendGroup/MapFRSApiInstances" />
          </maps>
        </aCS>
        <aCS name="PSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/Backend/BackendGroup/MapPSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="PSApiInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/Backend/BackendGroup/MapPSApiInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:FRSApi:FRSEndPoint">
          <toPorts>
            <inPortMoniker name="/Backend/BackendGroup/FRSApi/FRSEndPoint" />
          </toPorts>
        </lBChannel>
        <lBChannel name="LB:PSApi:PSEndPoint">
          <toPorts>
            <inPortMoniker name="/Backend/BackendGroup/PSApi/PSEndPoint" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapFRSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Backend/BackendGroup/FRSApi/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapFRSApiInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Backend/BackendGroup/FRSApiInstances" />
          </setting>
        </map>
        <map name="MapPSApi:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/Backend/BackendGroup/PSApi/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapPSApiInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/Backend/BackendGroup/PSApiInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="FRSApi" generation="1" functional="0" release="0" software="D:\Lab3\Backend\csx\Debug\roles\FRSApi" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="FRSEndPoint" protocol="http" portRanges="70" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;FRSApi&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;FRSApi&quot;&gt;&lt;e name=&quot;FRSEndPoint&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PSApi&quot;&gt;&lt;e name=&quot;PSEndPoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Backend/BackendGroup/FRSApiInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Backend/BackendGroup/FRSApiUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Backend/BackendGroup/FRSApiFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="PSApi" generation="1" functional="0" release="0" software="D:\Lab3\Backend\csx\Debug\roles\PSApi" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="-1" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="PSEndPoint" protocol="http" portRanges="90" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;PSApi&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;FRSApi&quot;&gt;&lt;e name=&quot;FRSEndPoint&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;PSApi&quot;&gt;&lt;e name=&quot;PSEndPoint&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/Backend/BackendGroup/PSApiInstances" />
            <sCSPolicyUpdateDomainMoniker name="/Backend/BackendGroup/PSApiUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/Backend/BackendGroup/PSApiFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="FRSApiUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="PSApiUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="FRSApiFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="PSApiFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="FRSApiInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="PSApiInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="0d85e5b0-dc27-45ba-b61b-91d3b4dec74b" ref="Microsoft.RedDog.Contract\ServiceContract\BackendContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="fa9d4853-c3de-44ea-bb68-7850114c4b5f" ref="Microsoft.RedDog.Contract\Interface\FRSApi:FRSEndPoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Backend/BackendGroup/FRSApi:FRSEndPoint" />
          </inPort>
        </interfaceReference>
        <interfaceReference Id="0dfae7d6-d6e1-447c-8d6c-c9893adc656e" ref="Microsoft.RedDog.Contract\Interface\PSApi:PSEndPoint@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/Backend/BackendGroup/PSApi:PSEndPoint" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>