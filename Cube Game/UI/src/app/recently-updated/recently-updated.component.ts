import { Component, OnInit } from '@angular/core';
import { RecentlyUpdatedService } from '../../Services/recently-updated.service';

@Component({
  selector: 'app-recently-updated',
  templateUrl: './recently-updated.component.html',
  styleUrls: ['./recently-updated.component.css']
})
export class RecentlyUpdatedComponent implements OnInit{
  constructor(public myService:RecentlyUpdatedService){}
  RecentlyUpdated:any;
  ngOnInit(): void {
    this.myService.GetSomeRecentlyUpdated().subscribe({
      next:(data)=>{
        this.RecentlyUpdated = data;
      },
      error:(err)=>{
        console.log(err);

      }
    });
  }

}
