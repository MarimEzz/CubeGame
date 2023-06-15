import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OrderService } from 'src/Services/order.service';

@Component({
  selector: 'app-library',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.css']
})
export class LibraryComponent implements OnInit{

  AllOrders : any
  constructor(private router: Router ,
    public orderService : OrderService){}

  ngOnInit(): void {

   this.orderService.GetOrderForUser().subscribe({
      next:(data)=>{

         this.AllOrders = data
      },
      error:(err)=>{console.log(err)}
    })
  }


}
