import { Component, OnInit } from '@angular/core';
import { MostPlayedService } from '../../Services/most-played.service';

@Component({
  selector: 'app-most-played',
  templateUrl: './most-played.component.html',
  styleUrls: ['./most-played.component.css']
})
export class MostPlayedComponent implements OnInit{
  constructor(public myService:MostPlayedService){}
  mostPlayed:any
  ngOnInit(): void {
    this.myService.GetSomeMostPlayed().subscribe({
      next:(data)=>{
        this.mostPlayed = data;
      },
      error:(err)=>{
        console.log(err);

      }
    });
  }
}
