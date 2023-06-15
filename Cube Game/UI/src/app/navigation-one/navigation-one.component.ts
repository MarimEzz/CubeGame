import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs/internal/Subscription';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { UserStoreService } from 'src/Services/user-store.service';
import { WishlistService } from 'src/Services/wishlist.service';
import { CategoryNameService} from 'src/Services/category-name.service'

@Component({
  selector: 'app-navigation-one',
  templateUrl: './navigation-one.component.html',
  styleUrls: ['./navigation-one.component.css']
})
export class NavigationOneComponent implements OnInit ,OnDestroy{

    catg:any;

  public fullName:string =""

  total : number =0
  total1:number=0;
  Logged:boolean = false;
  private cartItemsSubscription: Subscription = new Subscription;
  private wishlistItemsSubscription: Subscription = new Subscription;
  constructor(private user_Store: UserStoreService , private auth : AuthService ,
    private route:Router , private cartService : CartService,private wishlistService:WishlistService ,
    public category:CategoryNameService){}

  ngOnInit(): void {

    this.category.GetAllcategoryname().subscribe({
      next:(data)=>{
        this.catg=data;
      },
      error:(err)=>{console.log(err)}
    })



    if (this.auth.IsLoggedIn()&& this.auth.getRoleFromToken()!=='Admin') {
      this.Logged = true;

      this.cartItemsSubscription = this.cartService.cartItems$.subscribe(
        (cartItems) => {
          this.total = cartItems.length;
        }
      );

      this.cartService.GetCart().subscribe();

      this.wishlistItemsSubscription=this.wishlistService.wishlistItems$.subscribe(
        (wishlistItems) => {
          this.total1 = wishlistItems.length;
        }
      )

       this.wishlistService.GetWishlist().subscribe();

       this.user_Store.getFullNameFromStore().subscribe({
         next:(data)=>{

           let fullnameFromToken = this.auth.getFullNameFromToken();
           this.fullName = data || fullnameFromToken
         }
       })
   }
  //  ///////////////////////////////////////

  }
   ngOnDestroy(): void {
    this.cartItemsSubscription.unsubscribe();
    this.wishlistItemsSubscription.unsubscribe();
  }

  logOut(){
    this.auth.logOut();
    this.route.navigate(['Browse'])
    window.location.reload()
  }

}




