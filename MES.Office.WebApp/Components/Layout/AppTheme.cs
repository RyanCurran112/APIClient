using MudBlazor;

namespace MES.Office.WebApp.Components.Layout;

/// <summary>
/// MES Office WebApp theme configuration.
/// Provides consistent theming aligned with the WMS project's design system.
/// The WebApp uses an admin-focused variant with the same color palette.
/// </summary>
public static class AppTheme
{
    /// <summary>
    /// Primary brand color (teal/green) - MES Office brand identity
    /// </summary>
    public const string PrimaryLight = "#00967E";
    public const string PrimaryDark = "#28BD98";

    /// <summary>
    /// Secondary color for supporting elements
    /// </summary>
    public const string SecondaryLight = "#6B7280";
    public const string SecondaryDark = "#71717A";

    /// <summary>
    /// Tertiary/accent color for highlights
    /// </summary>
    public const string Tertiary = "#f7931a";

    /// <summary>
    /// Gets the MES Office theme with light and dark mode palettes.
    /// </summary>
    public static MudTheme Theme { get; } = new MudTheme
    {
        PaletteLight = new PaletteLight
        {
            // Brand Colors
            Primary = PrimaryLight,
            Secondary = SecondaryLight,
            Tertiary = Tertiary,

            // App Bar
            AppbarBackground = "#FFFFFF",
            AppbarText = "#1F2937",

            // Backgrounds
            Background = "#F9FAFB",
            Surface = "#FFFFFF",

            // Drawer
            DrawerBackground = "#FFFFFF",
            DrawerText = "#1F2937",
            DrawerIcon = "#6B7280",

            // Text
            TextPrimary = "#111827",
            TextSecondary = "#6B7280",

            // Actions
            ActionDefault = "#6B7280",
            ActionDisabled = "#D1D5DB",
            ActionDisabledBackground = "#F3F4F6",

            // Dividers & Lines
            Divider = "#E5E7EB",
            DividerLight = "#F3F4F6",
            TableLines = "#E5E7EB",
            LinesDefault = "#E5E7EB",
            LinesInputs = "#D1D5DB",

            // Semantic Colors
            Success = "#28BD98",
            Error = "#EF5350",
            Warning = "#F59E0B",
            Info = "#3B82F6",

            // Hover States
            HoverOpacity = 0.04,
            RippleOpacity = 0.08,
            RippleOpacitySecondary = 0.04
        },

        PaletteDark = new PaletteDark
        {
            // Brand Colors
            Primary = PrimaryDark,
            Secondary = SecondaryDark,
            Tertiary = Tertiary,

            // App Bar
            AppbarBackground = "#0D0D0D",
            AppbarText = "#FFFFFF",

            // Backgrounds
            Background = "#000000",
            BackgroundGray = "#0D0D0D",
            Surface = "#181A1F",

            // Drawer
            DrawerBackground = "#000000",
            DrawerText = "#FFFFFF",
            DrawerIcon = "#A1A1AA",

            // Text
            TextPrimary = "#FFFFFF",
            TextSecondary = "rgba(255,255,255,0.6)",

            // Actions
            ActionDefault = "#A1A1AA",
            ActionDisabled = "#52525B",
            ActionDisabledBackground = "#27272A",

            // Dividers & Lines
            Divider = "rgba(255,255,255,0.1)",
            DividerLight = "rgba(255,255,255,0.05)",
            TableLines = "rgba(255,255,255,0.1)",
            LinesDefault = "rgba(255,255,255,0.1)",
            LinesInputs = "rgba(255,255,255,0.2)",

            // Semantic Colors
            Success = "#28BD98",
            Error = "#EF5350",
            Warning = "#F59E0B",
            Info = "#3B82F6",

            // Hover States
            HoverOpacity = 0.06,
            RippleOpacity = 0.10,
            RippleOpacitySecondary = 0.06
        },

        // Typography settings for consistent text rendering
        Typography = new Typography
        {
            Default = new DefaultTypography
            {
                FontFamily = new[] { "system-ui", "-apple-system", "BlinkMacSystemFont", "'Segoe UI'", "Roboto", "sans-serif" },
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.5"
            },
            H1 = new H1Typography
            {
                FontSize = "2rem",
                FontWeight = "600",
                LineHeight = "1.25"
            },
            H2 = new H2Typography
            {
                FontSize = "1.5rem",
                FontWeight = "600",
                LineHeight = "1.3"
            },
            H3 = new H3Typography
            {
                FontSize = "1.25rem",
                FontWeight = "600",
                LineHeight = "1.35"
            },
            H4 = new H4Typography
            {
                FontSize = "1.125rem",
                FontWeight = "600",
                LineHeight = "1.4"
            },
            H5 = new H5Typography
            {
                FontSize = "1rem",
                FontWeight = "600",
                LineHeight = "1.5"
            },
            H6 = new H6Typography
            {
                FontSize = "0.875rem",
                FontWeight = "600",
                LineHeight = "1.5"
            },
            Body1 = new Body1Typography
            {
                FontSize = "0.875rem",
                FontWeight = "400",
                LineHeight = "1.5"
            },
            Body2 = new Body2Typography
            {
                FontSize = "0.8125rem",
                FontWeight = "400",
                LineHeight = "1.5"
            },
            Caption = new CaptionTypography
            {
                FontSize = "0.75rem",
                FontWeight = "400",
                LineHeight = "1.5"
            },
            Button = new ButtonTypography
            {
                FontSize = "0.875rem",
                FontWeight = "500",
                LineHeight = "1.5",
                TextTransform = "none"
            }
        },

        // Layout defaults
        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "6px",
            DrawerWidthLeft = "260px",
            DrawerMiniWidthLeft = "56px"
        },

        // Z-index layering
        ZIndex = new ZIndex
        {
            Drawer = 1100,
            AppBar = 1200,
            Dialog = 1300,
            Popover = 1400,
            Snackbar = 1500,
            Tooltip = 1600
        }
    };

    /// <summary>
    /// Admin-specific color constants for data seeding tool.
    /// These can be used for status indicators and special UI elements.
    /// </summary>
    public static class AdminColors
    {
        public const string Seeding = "#3B82F6";      // Blue - for seeding operations
        public const string Testing = "#8B5CF6";      // Purple - for testing operations
        public const string Success = "#28BD98";      // Green - for successful operations
        public const string Warning = "#F59E0B";      // Amber - for warnings
        public const string Error = "#EF5350";        // Red - for errors
        public const string Info = "#0891B2";         // Cyan - for informational messages
    }

    /// <summary>
    /// Status colors for workflow and entity states.
    /// Matches the WMS design tokens.
    /// </summary>
    public static class StatusColors
    {
        public const string Draft = "#6B7280";        // Gray
        public const string Pending = "#D97706";      // Amber
        public const string Approved = "#059669";     // Green
        public const string Rejected = "#DC2626";     // Red
        public const string Completed = "#0891B2";    // Cyan
        public const string Cancelled = "#9CA3AF";    // Light Gray
    }
}
