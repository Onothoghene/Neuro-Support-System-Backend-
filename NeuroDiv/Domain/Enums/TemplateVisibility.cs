namespace Domain.Enums
{
    public enum TemplateVisibility
    {
        Private = 1,   // only creator can use
        OrgWide = 2,   // shared across the org
        System = 3    // platform-provided, available to all
    }
}
