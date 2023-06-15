import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { ProductBrowseService } from 'src/Services/product-browse.service';
import { WishlistService } from 'src/Services/wishlist.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})

export class GameComponent implements OnInit{

  ID:any;

  product:any;

  img:any;

  sliderImages = document.getElementById('slider-images');

  a = window.matchMedia("(min-width: 991px)");




  goToTop(){

    this.sliderImages?.scroll

    if (this.a.matches) {

      this.sliderImages?.scrollBy(0,-5) ;

    } else {

      this.sliderImages?.scrollBy(-5,0) ;

    }

  }

  goToDown(){

    if (this.a.matches) {

      this.sliderImages?.scrollBy(0,5) ;

    } else {

      this.sliderImages?.scrollBy(5,0) ;

    }




  }



  constructor(myRoute:ActivatedRoute,public myServices:ProductBrowseService,private auth : AuthService,private cartService :CartService,private route:Router,private wishlistService: WishlistService){

    this.ID = myRoute.snapshot.params["id"];

  }
  ngOnInit(): void {
    this.myServices.GetProductByID(this.ID).subscribe({
      next:(data)=>{

        this.product = data;
      },
      error:(err)=>{console.log(err)}
    });

    this.myServices.GetProductImageByID(this.ID).subscribe({

      next:(data)=>{

        // console.log(data)

        this.img = data;

      },

      error:(err)=>{console.log(err)}

    })
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
 addToWishlist(id : any) {
  if(this.auth.IsLoggedIn()){
    this.wishlistService.AddToWishlist(id).subscribe(
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
