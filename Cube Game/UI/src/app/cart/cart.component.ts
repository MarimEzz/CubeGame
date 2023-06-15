import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { CartService  , Product} from 'src/Services/cart.service';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit{

  public Total : number =0
  AllProductCart: Product[] = [];
  constructor(private myService : CartService){}

  ngOnInit(): void {
    this.myService.GetCart().subscribe({
      next: (items) => {
        console.log('Cart items:', items);
        this.AllProductCart = items;
       items.forEach(element => {
       this.Total += element.priceAfterDiscount;
       });

      },
      error: (err) => {
        console.error('Error:', err);
      }
    });
  }

  removeItem(id: number) {
    if (id) {
      this.myService.RemoveFCart(id).subscribe({
        next: () => {
          console.log('Item removed from cart:', id);
          const index = this.AllProductCart.findIndex(item => item.id === id);
          if (index !== -1) {
            this.Total -=this.AllProductCart[index].priceAfterDiscount;
            this.AllProductCart.splice(index, 1);

          }
        },
        error: (err) => {
          console.error('Error:', err);
        }
      });
    }
  }

  emptycart(){
    this.myService.ClearCart().subscribe({
      next: (items) => {

        this.AllProductCart = [];
      },
      error: (err) => {
        console.error('Error:', err);
      }
    });
  }
}

