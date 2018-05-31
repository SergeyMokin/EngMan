import { Directive, HostBinding, HostListener } from "@angular/core";

@Directive(
    {
        selector: '[hoveredRule]'
    }
)
export class HoveredRuleDirective
{
    @HostBinding("class.hovered-rule") isHovered = false;

    @HostListener("mouseenter") onMouseEnter()
    {
        this.isHovered = true;
    }

    @HostListener("mouseleave") onMouseLeave()
    {
        this.isHovered = false;
    }
}