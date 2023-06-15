import { Component, OnInit } from '@angular/core';
import { TopRatedService } from '../../Services/top-rated.service';

@Component({
  selector: 'app-top-rated',
  templateUrl: './top-rated.component.html',
  styleUrls: ['./top-rated.component.css']
})
export class TopRatedComponent implements OnInit{
  constructor(public myService:TopRatedService){}
  TopRated:any;
  ngOnInit(): void {
    this.myService.GetSomeTopRated().subscribe({
      next:(data)=>{
        this.TopRated = data;
      },
      error:(err)=>{
        console.log(err);

      }
    })
  }

}
