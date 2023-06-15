import { Component } from '@angular/core';
import { MostPopularService } from '../../Services/most-popular.service';

@Component({
  selector: 'app-dist-filmslidel',
  templateUrl: './dist-filmslidel.component.html',
  styleUrls: ['./dist-filmslidel.component.css']
})
export class DistFilmslidelComponent {
  constructor(public myService:MostPopularService){}
  Mostpop:any;

  multislideConfig=
  {
    slidesToShow:5,
    slidesToScroll:1,
    dots:false,
    infinite:true,
    arrows:false,
    autoplay:true,
    autoplaySpeed: 700,
  }

  ngOnInit(): void {
    this.myService.GetAllMostPopular().subscribe({
      next: (data) => {
        this.Mostpop = data;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
