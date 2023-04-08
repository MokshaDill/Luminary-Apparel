using System;
using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

public class PlaceholderTextBox : KryptonTextBox
{
    private string _placeholderText = string.Empty;
    private bool _showPlaceholder = true;

    public string PlaceholderText
    {
        get { return _placeholderText; }
        set
        {
            _placeholderText = value;
            Invalidate();
        }
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);

        if (m.Msg == 0xf || m.Msg == 0x133) // WM_PAINT || WM_NCPAINT
        {
            using (var graphics = Graphics.FromHwnd(Handle))
            {
                if (_showPlaceholder && string.IsNullOrEmpty(Text) && !string.IsNullOrEmpty(_placeholderText))
                {
                    using (var brush = new SolidBrush(Color.Gray))
                    {
                        var format = new StringFormat();
                        format.LineAlignment = StringAlignment.Center;
                        format.Alignment = TextAlign.ToContentAlignment();
                        graphics.DrawString(_placeholderText, Font, brush, new RectangleF(Padding.Left, Padding.Top, Width - Padding.Horizontal, Height - Padding.Vertical), format);
                    }
                }
            }
        }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        _showPlaceholder = string.IsNullOrEmpty(Text);
        Invalidate();
    }
}

internal static class Extensions
{
    public static StringAlignment ToContentAlignment(this HorizontalAlignment alignment)
    {
        switch (alignment)
        {
            case HorizontalAlignment.Center:
                return StringAlignment.Center;
            case HorizontalAlignment.Left:
                return StringAlignment.Near;
            case HorizontalAlignment.Right:
                return StringAlignment.Far;
            default:
                return StringAlignment.Near;
        }
    }
}
