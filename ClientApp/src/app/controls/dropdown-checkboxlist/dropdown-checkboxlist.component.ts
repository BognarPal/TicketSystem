// dokumentáció: https://alligator.io/angular/custom-form-control/
import { Component, OnInit, forwardRef, Input, ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor } from '@angular/forms';

@Component({
  selector: 'app-dropdown-checkboxlist',
  templateUrl: './dropdown-checkboxlist.component.html',
  providers: [{
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => DropdownCheckboxlistComponent),
    multi: true
  }]
})
export class DropdownCheckboxlistComponent implements ControlValueAccessor, OnInit {
  @Input() disabled = false;
  @Input() maxBadge = 3;
  @Input() set listSource(listSource: any[]) {
    this.list = [];
    listSource.forEach(l => {
      const newItem = JSON.parse(JSON.stringify(l));
      newItem.selected = false;
      this.list.push(newItem);
    });
  }
  @Input() valueMemberPath = 'id';
  @Input() displayMemberPath = 'name';

  list: any[] = [];
  checkedItems: any[] = [];
  componentName = '';

  constructor(private elementRef: ElementRef) { }

  ngOnInit() {
    this.componentName = this.elementRef.nativeElement.attributes['name'].value;
  }

  onChange = (checked: any[]) => {};
  onTouched = () => {};

  checkChange(itemValue, checked) {
    if (checked) {
      this.checkedItems.push(itemValue);
    } else {
      const index = this.checkedItems.indexOf(itemValue);
      if (index !== -1) {
        this.checkedItems.splice(index, 1);
      }
    }
  }

  writeValue(checkedItems: any[]): void {
    this.checkedItems = checkedItems || [];
    this.onChange(checkedItems);
  }

  registerOnChange(fn: (checked: any[]) => void): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  get textOfButton(): string {
    if (this.checkedItems.length > this.maxBadge) {
      return 'Kiválasztott elemek száma: ' + this.checkedItems.length.toString() + ' db';
    } else {
      return '';
    }
  }

  displayTextFromValue(value) {
    if (value) {
      const s = this.list.filter(li => li[this.valueMemberPath] === value);
      if (s && s.length > 0) {
        return s[0][this.displayMemberPath];
      }
    }
    return '';
  }

}
