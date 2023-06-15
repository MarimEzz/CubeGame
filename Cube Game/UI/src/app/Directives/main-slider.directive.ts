import { Directive, HostListener, ElementRef} from '@angular/core';

@Directive({
  selector: '[appMainSlider]'
})
export class MainSliderDirective {

  constructor(private el: ElementRef) { }

  @HostListener('click')
  imagechange(){
    var src:any =this.el.nativeElement.src;
    var prev:any = document.getElementById("preview");
    prev.src = src;
    var imgslide = document.getElementsByClassName("img-slide");
    for(let i=0; i < imgslide.length; i++)
    {
      imgslide[i].classList.remove("active-slide");
    }
    this.el.nativeElement.parentElement.classList.add("active-slide");
  }
}
