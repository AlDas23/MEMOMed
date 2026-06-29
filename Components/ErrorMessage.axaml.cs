using Avalonia;
using Avalonia.Controls.Primitives;

namespace MEMOMed.Components;

public class ErrorMessage : TemplatedControl
{
    public static readonly StyledProperty<string> MessageProperty =
        AvaloniaProperty.Register<ErrorMessage, string>(nameof(Message));

    public static readonly StyledProperty<bool> IsErrorProperty =
        AvaloniaProperty.Register<ErrorMessage, bool>(nameof(IsError));

    public string Message
    {
        get => GetValue(MessageProperty);
        set => SetValue(MessageProperty, value);
    }

    public bool IsError
    {
        get => GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }
}