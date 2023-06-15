import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { Product, WishlistService } from 'src/Services/wishlist.service';

@Component({
  selector: 'app-wishlist',
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {


  AllProduct: any[] = [];
  constructor(private wishlistService: WishlistService, private cartService :CartService,private auth : AuthService ,private route:Router ) { }
  ngOnInit(): void {
    this.wishlistService.GetWishlist().subscribe(
      {
        next: (items) => {
          console.log('wish list ',items)
          this.AllProduct = items;

        },
        error: (err) => {
          console.error('Error:', err);
        }
      }
    )

  }
  deleteitem(id:number) {
      if(id){
        this.wishlistService.DeleteFromWishlist(id).subscribe(
       { next:()=>{
        console.log('delete item ',id);
        const index=this.AllProduct.findIndex( item=>item.id===id);
        if(index!==1)
        {this.AllProduct.splice(index,1);
        }},
        error: (err) => {
          console.error('Error:', err);
        }

       })


      }

  }

clearWishlist(){
this.wishlistService.ClearCart().subscribe(
  {
    next:(item)=>{
      this.AllProduct=[];
  },
  error: (err) => {
    console.error('Error:', err);
  }

}
)

}

 addcart(id:any){

  if(this.auth.IsLoggedIn()){

   this.cartService.AddTCart(id).subscribe(

    (response) => {

   console.log('Response:', response);

 },

   (error) => {

     console.error('Error:', error);

    }

  );

     }

     else{

    this.route.navigate(['Login'])

    }

  }
 }



















































