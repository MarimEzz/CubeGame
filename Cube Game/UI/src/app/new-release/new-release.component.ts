import { Component, OnInit } from '@angular/core';
import { NewReleaseService } from '../../Services/new-release.service';

@Component({
  selector: 'app-new-release',
  templateUrl: './new-release.component.html',
  styleUrls: ['./new-release.component.css']
})
export class NewReleaseComponent  implements OnInit{
  constructor(public myService:NewReleaseService){}
  NewRelease:any;
  ngOnInit(): void {
    this.myService.GetSomeNewRelease().subscribe({
      next:(data)=>{
        this.NewRelease = data;
      },
      error:(err)=>{
        console.log(err);

      }
    })
  }
}
