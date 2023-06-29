using System.ComponentModel;

namespace VideoPlatform.Domain.Enums;

public enum MetaType : byte
{
    [Description("Document")] Document = 1,
    [Description("File")] File = 2,
    [Description("Form")] Form = 3
}