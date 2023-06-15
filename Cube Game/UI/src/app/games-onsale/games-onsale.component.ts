import { Component, OnInit } from '@angular/core';
import { GamesOnsaleService } from '../../Services/games-onsale.service';

@Component({
  selector: 'app-games-onsale',
  templateUrl: './games-onsale.component.html',
  styleUrls: ['./games-onsale.component.css']
})
export class GamesOnsaleComponent implements OnInit {

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

  constructor(public myserv:GamesOnsaleService){}
  user:any;
  ngOnInit(): void {
    this.myserv.GetAllGamesOnSale().subscribe({
      next:(data)=>{
        this.user=data;
      },
      error:(err)=>{console.log(err)}
    })

  }
}
