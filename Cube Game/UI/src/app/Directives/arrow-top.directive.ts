import { Directive, HostListener , ElementRef } from '@angular/core';

@Directive({
  selector: '[appArrowTop]'
})
export class ArrowTopDirective {
  uparrow = document.getElementById('top-arrow');
  sliderImages = document.getElementById('slider-images');


  constructor(el:ElementRef) { }
  x:number = 0 ;
  y:number = 5*-1;
@HostListener('click')

scrollToTop(){
  var a = window.matchMedia("(min-width: 991px)")

  if (a.matches) {
    this.sliderImages?.scrollBy(this.x,this.y) ;
    let b = this.sliderImages?.scrollTop;
  } else {
    this.sliderImages?.scrollBy(this.y,this.x) ;
  }



}


}
