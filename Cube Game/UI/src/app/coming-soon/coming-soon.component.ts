import { Component, OnInit } from '@angular/core';
import { ComingSoonService } from '../../Services/coming-soon.service';

@Component({
  selector: 'app-coming-soon',
  templateUrl: './coming-soon.component.html',
  styleUrls: ['./coming-soon.component.css']
})
export class ComingSoonComponent implements OnInit{
  constructor(public myService:ComingSoonService){}
  ComingSoon:any
  ngOnInit(): void {
    this.myService.GetSomeComingSoon().subscribe({
      next:(data)=>{
        this.ComingSoon = data;
      },
      error:(err)=>{
        console.log(err);

      }
    });
  }
}


