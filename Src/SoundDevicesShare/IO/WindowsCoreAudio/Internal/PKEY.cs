using System;
using System.Collections.Generic;
using System.Text;

namespace SoundDevices.IO.WindowsCoreAudio.Internal
{
    internal static class PKEY
    {
        // Audio Endpoint Properties
        public const int ENDPOINT_SYSFX_ENABLED = 0x00000000;
        public const int ENDPOINT_SYSFX_DISABLED = 0x00000001;

        private static readonly Guid GUID_AudioEndpoint = new Guid(0x1da5d803, 0xd492, 0x4edd, 0x8c, 0x23, 0xe0, 0xc0, 0xff, 0xee, 0x7f, 0x0e);
        private static readonly Guid GUID_Device = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0);
        private static readonly Guid GUID_DeviceInterface = new Guid(0x026e516e, 0xb814, 0x414b, 0x83, 0xcd, 0x85, 0x6d, 0x6f, 0xef, 0x48, 0x22);

        public static readonly PROPERTYKEY PKEY_AudioEndpoint_FormFactor = new PROPERTYKEY(GUID_AudioEndpoint, 0);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_ControlPanelPageProvider = new PROPERTYKEY(GUID_AudioEndpoint, 1);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_Association = new PROPERTYKEY(GUID_AudioEndpoint, 2);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_PhysicalSpeakers = new PROPERTYKEY(GUID_AudioEndpoint, 3);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_GUID = new PROPERTYKEY(GUID_AudioEndpoint, 4);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_Disable_SysFx = new PROPERTYKEY(GUID_AudioEndpoint, 5);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_FullRangeSpeakers = new PROPERTYKEY(GUID_AudioEndpoint, 6);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_Supports_EventDriven_Mode = new PROPERTYKEY(GUID_AudioEndpoint, 7);
        public static readonly PROPERTYKEY PKEY_AudioEndpoint_JackSubType = new PROPERTYKEY(GUID_AudioEndpoint, 8);

        public static readonly PROPERTYKEY PKEY_AudioEngine_DeviceFormat = new PROPERTYKEY(new Guid(0xf19f064d, 0x82c, 0x4e27, 0xbc, 0x73, 0x68, 0x82, 0xa1, 0xbb, 0x8e, 0x4c), 0);
        public static readonly PROPERTYKEY PKEY_AudioEngine_OEMFormat = new PROPERTYKEY(new Guid(0xe4870e26, 0x3cc5, 0x4cd2, 0xba, 0x46, 0xca, 0xa, 0x9a, 0x70, 0xed, 0x4), 3);

        // Device Properties
        // These PKEYs correspond to the old setupapi SPDRP_XXX properties
        public static readonly PROPERTYKEY PKEY_Device_DeviceDesc = new PROPERTYKEY(GUID_Device, 2);
        public static readonly PROPERTYKEY PKEY_Device_HardwareIds = new PROPERTYKEY(GUID_Device, 3);
        public static readonly PROPERTYKEY PKEY_Device_CompatibleIds = new PROPERTYKEY(GUID_Device, 4);
        public static readonly PROPERTYKEY PKEY_Device_Service = new PROPERTYKEY(GUID_Device, 6);
        public static readonly PROPERTYKEY PKEY_Device_Class = new PROPERTYKEY(GUID_Device, 9);
        public static readonly PROPERTYKEY PKEY_Device_ClassGuid = new PROPERTYKEY(GUID_Device, 10);
        public static readonly PROPERTYKEY PKEY_Device_Driver = new PROPERTYKEY(GUID_Device, 11);
        public static readonly PROPERTYKEY PKEY_Device_ConfigFlags = new PROPERTYKEY(GUID_Device, 12);
        public static readonly PROPERTYKEY PKEY_Device_Manufacturer = new PROPERTYKEY(GUID_Device, 13);
        public static readonly PROPERTYKEY PKEY_Device_FriendlyName = new PROPERTYKEY(GUID_Device, 14);
        public static readonly PROPERTYKEY PKEY_Device_LocationInfo = new PROPERTYKEY(GUID_Device, 15);
        public static readonly PROPERTYKEY PKEY_Device_PDOName = new PROPERTYKEY(GUID_Device, 16);
        public static readonly PROPERTYKEY PKEY_Device_Capabilities = new PROPERTYKEY(GUID_Device, 17);
        public static readonly PROPERTYKEY PKEY_Device_UINumber = new PROPERTYKEY(GUID_Device, 18);
        public static readonly PROPERTYKEY PKEY_Device_UpperFilters = new PROPERTYKEY(GUID_Device, 19);
        public static readonly PROPERTYKEY PKEY_Device_LowerFilters = new PROPERTYKEY(GUID_Device, 20);
        public static readonly PROPERTYKEY PKEY_Device_BusTypeGuid = new PROPERTYKEY(GUID_Device, 21);
        public static readonly PROPERTYKEY PKEY_Device_LegacyBusType = new PROPERTYKEY(GUID_Device, 22);
        public static readonly PROPERTYKEY PKEY_Device_BusNumber = new PROPERTYKEY(GUID_Device, 23);
        public static readonly PROPERTYKEY PKEY_Device_EnumeratorName = new PROPERTYKEY(GUID_Device, 24);
        public static readonly PROPERTYKEY PKEY_Device_Security = new PROPERTYKEY(GUID_Device, 25);
        public static readonly PROPERTYKEY PKEY_Device_SecuritySDS = new PROPERTYKEY(GUID_Device, 26);
        public static readonly PROPERTYKEY PKEY_Device_DevType = new PROPERTYKEY(GUID_Device, 27);
        public static readonly PROPERTYKEY PKEY_Device_Exclusive = new PROPERTYKEY(GUID_Device, 28);
        public static readonly PROPERTYKEY PKEY_Device_Characteristics = new PROPERTYKEY(GUID_Device, 29);
        public static readonly PROPERTYKEY PKEY_Device_Address = new PROPERTYKEY(GUID_Device, 30);
        public static readonly PROPERTYKEY PKEY_Device_UINumberDescFormat = new PROPERTYKEY(GUID_Device, 31);
        public static readonly PROPERTYKEY PKEY_Device_PowerData = new PROPERTYKEY(GUID_Device, 32);
        public static readonly PROPERTYKEY PKEY_Device_RemovalPolicy = new PROPERTYKEY(GUID_Device, 33);
        public static readonly PROPERTYKEY PKEY_Device_RemovalPolicyDefault = new PROPERTYKEY(GUID_Device, 34);
        public static readonly PROPERTYKEY PKEY_Device_RemovalPolicyOverride = new PROPERTYKEY(GUID_Device, 34);
        public static readonly PROPERTYKEY PKEY_Device_InstallState = new PROPERTYKEY(GUID_Device, 36);
        public static readonly PROPERTYKEY PKEY_Device_LocationPaths = new PROPERTYKEY(GUID_Device, 37);
        public static readonly PROPERTYKEY PKEY_Device_BaseContainerId = new PROPERTYKEY(GUID_Device, 38);

        // Device properties
        // These PKEYs correspond to a device's status and problem code
        public static readonly PROPERTYKEY PKEY_Device_DevNodeStatus = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 2);
        public static readonly PROPERTYKEY PKEY_Device_ProblemCode = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 3);

        // Device properties
        // These PKEYs correspond to device relations
        public static readonly PROPERTYKEY PKEY_Device_EjectionRelations = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 4);
        public static readonly PROPERTYKEY PKEY_Device_RemovalRelations = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 5);
        public static readonly PROPERTYKEY PKEY_Device_PowerRelations = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 6);
        public static readonly PROPERTYKEY PKEY_Device_BusRelations = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 7);
        public static readonly PROPERTYKEY PKEY_Device_Parent = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 8);
        public static readonly PROPERTYKEY PKEY_Device_Children = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 9);
        public static readonly PROPERTYKEY PKEY_Device_Siblings = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 10);
        public static readonly PROPERTYKEY PKEY_Device_TransportRelations = new PROPERTYKEY(new Guid(0x4340a6c5, 0x93fa, 0x4706, 0x97, 0x2c, 0x7b, 0x64, 0x80, 0x08, 0xa5, 0xa7), 11);


        // Other Device properties
        public static readonly PROPERTYKEY PKEY_Device_Reported = new PROPERTYKEY(new Guid(0x80497100, 0x8c73, 0x48b9, 0xaa, 0xd9, 0xce, 0x38, 0x7e, 0x19, 0xc5, 0x6e), 2);
        public static readonly PROPERTYKEY PKEY_Device_Legacy = new PROPERTYKEY(new Guid(0x80497100, 0x8c73, 0x48b9, 0xaa, 0xd9, 0xce, 0x38, 0x7e, 0x19, 0xc5, 0x6e), 3);
        public static readonly PROPERTYKEY PKEY_Device_InstanceId = new PROPERTYKEY(new Guid(0x78c34fc8, 0x104a, 0x4aca, 0x9e, 0xa4, 0x52, 0x4d, 0x52, 0x99, 0x6e, 0x57), 256);

        public static readonly PROPERTYKEY PKEY_Device_ContainerId = new PROPERTYKEY(new Guid(0x8c7ed206, 0x3f8a, 0x4827, 0xb3, 0xab, 0xae, 0x9e, 0x1f, 0xae, 0xfc, 0x6c), 2);

        public static readonly PROPERTYKEY PKEY_Device_ModelId = new PROPERTYKEY(new Guid(0x80d81ea6, 0x7473, 0x4b0c, 0x82, 0x16, 0xef, 0xc1, 0x1a, 0x2c, 0x4c, 0x8b), 2);
        public static readonly PROPERTYKEY PKEY_Device_FriendlyNameAttributes = new PROPERTYKEY(new Guid(0x80d81ea6, 0x7473, 0x4b0c, 0x82, 0x16, 0xef, 0xc1, 0x1a, 0x2c, 0x4c, 0x8b), 3);
        public static readonly PROPERTYKEY PKEY_Device_ManufacturerAttributes = new PROPERTYKEY(new Guid(0x80d81ea6, 0x7473, 0x4b0c, 0x82, 0x16, 0xef, 0xc1, 0x1a, 0x2c, 0x4c, 0x8b), 4);
        public static readonly PROPERTYKEY PKEY_Device_PresenceNotForDevice = new PROPERTYKEY(new Guid(0x80d81ea6, 0x7473, 0x4b0c, 0x82, 0x16, 0xef, 0xc1, 0x1a, 0x2c, 0x4c, 0x8b), 5);

        public static readonly PROPERTYKEY PKEY_Numa_Proximity_Domain = new PROPERTYKEY(new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2), 1);
        public static readonly PROPERTYKEY PKEY_Device_DHP_Rebalance_Policy = new PROPERTYKEY(new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2), 2);
        public static readonly PROPERTYKEY PKEY_Device_Numa_Node = new PROPERTYKEY(new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2), 3);
        public static readonly PROPERTYKEY PKEY_Device_BusReportedDeviceDesc = new PROPERTYKEY(new Guid(0x540b947e, 0x8b40, 0x45bc, 0xa8, 0xa2, 0x6a, 0x0b, 0x89, 0x4c, 0xbd, 0xa2), 4);

        public static readonly PROPERTYKEY PKEY_Device_InstallInProgress = new PROPERTYKEY(new Guid(0x83da6326, 0x97a6, 0x4088, 0x94, 0x53, 0xa1, 0x92, 0x3f, 0x57, 0x3b, 0x29), 9);

        // Device driver properties
        public static readonly PROPERTYKEY PKEY_Device_DriverDate = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 2);
        public static readonly PROPERTYKEY PKEY_Device_DriverVersion = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 3);
        public static readonly PROPERTYKEY PKEY_Device_DriverDesc = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 4);
        public static readonly PROPERTYKEY PKEY_Device_DriverInfPath = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 5);
        public static readonly PROPERTYKEY PKEY_Device_DriverInfSection = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 6);
        public static readonly PROPERTYKEY PKEY_Device_DriverInfSectionExt = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 7);
        public static readonly PROPERTYKEY PKEY_Device_MatchingDeviceId = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 8);
        public static readonly PROPERTYKEY PKEY_Device_DriverProvider = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 9);
        public static readonly PROPERTYKEY PKEY_Device_DriverPropPageProvider = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 10);
        public static readonly PROPERTYKEY PKEY_Device_DriverCoInstallers = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 11);
        public static readonly PROPERTYKEY PKEY_Device_ResourcePickerTags = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 12);
        public static readonly PROPERTYKEY PKEY_Device_ResourcePickerExceptions = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 13);
        public static readonly PROPERTYKEY PKEY_Device_DriverRank = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 14);
        public static readonly PROPERTYKEY PKEY_Device_DriverLogoLevel = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 15);
        public static readonly PROPERTYKEY PKEY_Device_NoConnectSound = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 17);
        public static readonly PROPERTYKEY PKEY_Device_GenericDriverInstalled = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 18);
        public static readonly PROPERTYKEY PKEY_Device_AdditionalSoftwareRequested = new PROPERTYKEY(new Guid(0xa8b865dd, 0x2e3d, 0x4094, 0xad, 0x97, 0xe5, 0x93, 0xa7, 0xc, 0x75, 0xd6), 19);

        // Device safe-removal properties
        public static readonly PROPERTYKEY PKEY_Device_SafeRemovalRequired = new PROPERTYKEY(new Guid(0xafd97640, 0x86a3, 0x4210, 0xb6, 0x7c, 0x28, 0x9c, 0x41, 0xaa, 0xbe, 0x55), 2);
        public static readonly PROPERTYKEY PKEY_Device_SafeRemovalRequiredOverride = new PROPERTYKEY(new Guid(0xafd97640, 0x86a3, 0x4210, 0xb6, 0x7c, 0x28, 0x9c, 0x41, 0xaa, 0xbe, 0x55), 3);

        // Device properties that were set by the driver package that was installed
        // on the device.
        public static readonly PROPERTYKEY PKEY_DrvPkg_Model = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);
        public static readonly PROPERTYKEY PKEY_DrvPkg_VendorWebSite = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);
        public static readonly PROPERTYKEY PKEY_DrvPkg_DetailedDescription = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);
        public static readonly PROPERTYKEY PKEY_DrvPkg_DocumentationLink = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);
        public static readonly PROPERTYKEY PKEY_DrvPkg_Icon = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);
        public static readonly PROPERTYKEY PKEY_DrvPkg_BrandingIcon = new PROPERTYKEY(new Guid(0xcf73bb51, 0x3abf, 0x44a2, 0x85, 0xe0, 0x9a, 0x3d, 0xc7, 0xa1, 0x21, 0x32), 2);

        // Device setup class properties
        // These PKEYs correspond to the old setupapi SPCRP_XXX properties
        public static readonly PROPERTYKEY PKEY_DeviceClass_UpperFilters = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 19);
        public static readonly PROPERTYKEY PKEY_DeviceClass_LowerFilters = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 20);
        public static readonly PROPERTYKEY PKEY_DeviceClass_Security = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 25);
        public static readonly PROPERTYKEY PKEY_DeviceClass_SecuritySDS = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 26);
        public static readonly PROPERTYKEY PKEY_DeviceClass_DevType = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 27);
        public static readonly PROPERTYKEY PKEY_DeviceClass_Exclusive = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 28);
        public static readonly PROPERTYKEY PKEY_DeviceClass_Characteristics = new PROPERTYKEY(new Guid(0x4321918b, 0xf69e, 0x470d, 0xa5, 0xde, 0x4d, 0x88, 0xc7, 0x5a, 0xd2, 0x4b), 29);

        // Device setup class properties
        // These PKEYs correspond to registry values under the device class GUID key
        public static readonly PROPERTYKEY PKEY_DeviceClass_Name = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 2);
        public static readonly PROPERTYKEY PKEY_DeviceClass_ClassName = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 3);
        public static readonly PROPERTYKEY PKEY_DeviceClass_Icon = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 4);
        public static readonly PROPERTYKEY PKEY_DeviceClass_ClassInstaller = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 5);
        public static readonly PROPERTYKEY PKEY_DeviceClass_PropPageProvider = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 6);
        public static readonly PROPERTYKEY PKEY_DeviceClass_NoInstallClass = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 7);
        public static readonly PROPERTYKEY PKEY_DeviceClass_NoDisplayClass = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 8);
        public static readonly PROPERTYKEY PKEY_DeviceClass_SilentInstall = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 9);
        public static readonly PROPERTYKEY PKEY_DeviceClass_NoUseClass = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 10);
        public static readonly PROPERTYKEY PKEY_DeviceClass_DefaultService = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 11);
        public static readonly PROPERTYKEY PKEY_DeviceClass_IconPath = new PROPERTYKEY(new Guid(0x259abffc, 0x50a7, 0x47ce, 0xaf, 0x8, 0x68, 0xc9, 0xa7, 0xd7, 0x33, 0x66), 12);

        // Other Device setup class properties
        public static readonly PROPERTYKEY PKEY_DeviceClass_ClassCoInstallers = new PROPERTYKEY(new Guid(0x713d1703, 0xa2e2, 0x49f5, 0x92, 0x14, 0x56, 0x47, 0x2e, 0xf3, 0xda, 0x5c), 2);

        // Device interface properties
        public static readonly PROPERTYKEY PKEY_DeviceInterface_FriendlyName = new PROPERTYKEY(GUID_DeviceInterface, 2);
        public static readonly PROPERTYKEY PKEY_DeviceInterface_Enabled = new PROPERTYKEY(GUID_DeviceInterface, 3);
        public static readonly PROPERTYKEY PKEY_DeviceInterface_ClassGuid = new PROPERTYKEY(GUID_DeviceInterface, 4);

        // Device interface class properties
        public static readonly PROPERTYKEY PKEY_DeviceInterfaceClass_DefaultInterface = new PROPERTYKEY(new Guid(0x14c83a99, 0x0b3f, 0x44b7, 0xbe, 0x4c, 0xa1, 0x78, 0xd3, 0x99, 0x05, 0x64), 2);

        private static readonly Guid GUID_Capture = new Guid(0xa45c254e, 0xdf1c, 0x4efd, 0x80, 0x20, 0x67, 0xd1, 0x46, 0xa8, 0x50, 0xe0);


        public static readonly PROPERTYKEY PKEY_Capture_DeviceName = new PROPERTYKEY(GUID_Capture, 2);
        public static readonly PROPERTYKEY PKEY_Capture_DeviceInterface = new PROPERTYKEY(GUID_Capture, 6);

    }
}
