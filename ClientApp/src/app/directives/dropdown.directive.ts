import { Directive, HostListener, ElementRef, Renderer2, OnInit } from '@angular/core';

@Directive({
  selector: '[appDropdown]'
})
export class DropdownDirective implements OnInit {
  isOpen = false;
  item: any;

  @HostListener('document:click', ['$event']) toggleOpen(event: Event) {
    if (this.item) {
      if (!this.isOpen || !this.item.contains(event.target)) {
        this.isOpen = this.elementRef.nativeElement.parentNode.contains(event.target) ? !this.isOpen : false;
      }

      if (this.isOpen) {
        this.renderer.addClass(this.item, 'show');
      } else {
        this.renderer.removeClass(this.item, 'show');
      }
    }
  }

  constructor(
    private elementRef: ElementRef,
    private renderer: Renderer2) { }

  ngOnInit(): void {
    this.item = this.elementRef.nativeElement.parentNode.querySelector('.app-dropdown');
  }

}
