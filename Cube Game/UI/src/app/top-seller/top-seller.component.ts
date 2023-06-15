import { Component, OnInit } from '@angular/core';
import { TopSellerService } from '../../Services/top-seller.service';

@Component({
  selector: 'app-top-seller',
  templateUrl: './top-seller.component.html',
  styleUrls: ['./top-seller.component.css']
})
export class TopSellerComponent implements OnInit{
  constructor(public myService:TopSellerService){}
  TopSeller:any;
  ngOnInit(): void {
    this.myService.GetSomeTopSeller().subscribe({
      next:(data)=>{
        this.TopSeller = data;
      },
      error:(err)=>{
        console.log(err);

      }
    })
  }
}
