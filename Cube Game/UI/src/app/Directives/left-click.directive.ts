import { Directive, HostListener , ElementRef } from '@angular/core';

@Directive({
  selector: '[appLeftClick]'
})
export class LeftClickDirective {
  btn:any = document.getElementById('btn')?.style.left;

  constructor(private el:ElementRef) { }
  @HostListener('click')
  leftClick() {
    this.el.nativeElement.nextElementSibling.nextElementSibling.style.left = '0';
    this.el.nativeElement.style.color = 'white';
    this.el.nativeElement.nextElementSibling.nextElementSibling.nextElementSibling.style.color = '#707070'

     this.btn = "0px !important";

     console.log('btn1 clicked');

   }

}
