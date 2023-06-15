import { Component, OnInit } from '@angular/core';
import { MostPopularService } from '../../Services/most-popular.service';

@Component({
  selector: 'app-most-popular',
  templateUrl: './most-popular.component.html',
  styleUrls: ['./most-popular.component.css']
})
export class MostPopularComponent implements OnInit {

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

    constructor(public myserv:MostPopularService){}
    user:any;
    ngOnInit(): void {
      this.myserv.GetAllMostPopular().subscribe({
        next:(data)=>{
          this.user=data;
        },
        error:(err)=>{console.log(err)}
      })

    }
}
