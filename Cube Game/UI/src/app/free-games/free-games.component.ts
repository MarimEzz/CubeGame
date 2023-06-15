import { Component, OnInit } from '@angular/core';
import { FreegamesService } from '../../Services/free-games.service';

@Component({
  selector: 'app-free-games',
  templateUrl: './free-games.component.html',
  styleUrls: ['./free-games.component.css']
})
export class FreeGamesComponent implements OnInit {

  multislideConfig=
  {
    slidesToShow:5,
    slidesToScroll:1,
    dots:false,
    infinite:true,
    arrows:true,
    autoplay:true,
    autoplayspeed:1000
  }

    constructor(public myserv:FreegamesService){}
    user:any;
    ngOnInit(): void {
      this.myserv.GetAllFreeGames().subscribe({
        next:(data)=>{
          this.user=data;
        },
        error:(err)=>{console.log(err)}
      })

    }
}
