import { Component, OnInit } from '@angular/core';
import { FreegamesService } from '../../Services/free-games.service';

@Component({
  selector: 'app-dist-filmslider',
  templateUrl: './dist-filmslider.component.html',
  styleUrls: ['./dist-filmslider.component.css']
})
export class DistFilmsliderComponent implements OnInit {

  multislideConfig=
  {
    slidesToShow:5,
    slidesToScroll:1,
    dots:false,
    infinite:true,
    arrows:false,
    autoplay:true,
    autoplaySpeed: 700,
    rtl: true
  }

    constructor(public myserv:FreegamesService){}
    user:any;
    
    ngOnInit(): void {
      this.myserv.GetAllFreeGames().subscribe({
        next: (data) => {
          this.user = data;
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
}
