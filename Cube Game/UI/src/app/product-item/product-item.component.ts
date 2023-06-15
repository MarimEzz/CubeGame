import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { WishlistService } from 'src/Services/wishlist.service';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent {

  constructor(private myservice : CartService , private auth : AuthService ,
     private route:Router,private wishlistservice:WishlistService){}

@Input() oneProduct : any

addToCart(id : any) {
  if(this.auth.IsLoggedIn()){
    this.myservice.AddTCart(id).subscribe(
      (response) => {
        console.log('Response:', response);
      },

      (error) => {
        console.error('Error:', error);
      }
      );}
  else{
    this.route.navigate(['Login'])
}}
addToWishlist(id : any) {
  if(this.auth.IsLoggedIn()){
    this.wishlistservice.AddToWishlist(id).subscribe(
      (response) => {
        console.log('Response:', response);
      },
      (error) => {
        console.error('Error:', error);
      }
    );
  }
  else{
    this.route.navigate(['Login'])}
}
}
