import { Directive, HostListener , ElementRef } from '@angular/core';

@Directive({
  selector: '[appArrowbutton]'
})
export class ArrowbuttonDirective {

  constructor(private el:ElementRef) { }

  downarrow = document.getElementById('down-arrow')
  sliderImages = document.getElementById('slider-images')

  @HostListener('click')
  scrollToDown(){

    var x = window.matchMedia("(min-width: 991px)")
    if (x.matches) {
      this.sliderImages?.scrollBy(0,5) ;
    } else {
      this.sliderImages?.scrollBy(5,0) ;
    }
  }
}
