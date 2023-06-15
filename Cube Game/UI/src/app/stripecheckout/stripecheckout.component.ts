import { Component } from '@angular/core';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { OrderService } from 'src/Services/order.service';
import { StripecheckoutService } from 'src/Services/stripecheckout.service';

@Component({
  selector: 'app-stripecheckout',
  templateUrl: './stripecheckout.component.html',
  styleUrls: ['./stripecheckout.component.css']
})
export class StripecheckoutComponent {
  title = "angularstripeapp";
  handler:any = null;
  success:boolean = false;
  failure:boolean = false;
  showPayButton:boolean = true;

  constructor(private checkout:StripecheckoutService ,
     private orderService : OrderService
     , private Auth : AuthService
     , private cartService : CartService){}

  ngOnInit(){
    this.invokeStrip()
  }

  makePayment(){
    var handler = (<any>window).StripeCheckout.configure({
      key: 'pk_test_51N96sVGXyzvXCyS2EriepHJroYOAvBjU6oml1UzutT6w69dpyNvihVqGwXjO4mWavAS4UQdCtwaafrK6VdbJWgao00Md3Ors8T',
      locale: 'auto',
      token: function (stripeToken: any) {
        console.log(stripeToken)
        // alert('Token Created!!');

        paymentStripe(stripeToken)
      }

     });


    const paymentStripe = (stripeToken: any) => {
       this.checkout.makePayment(stripeToken).subscribe((data:any) => {
            console.log(data)

            if(data.data === "success"){
                  this.success = true;
                  this.orderService.AddOrder().subscribe()
                  this.cartService.ClearCart().subscribe()
                  this.hidePayButton()
            }
            else{
               this.failure = true;
            }
       })
    }

    handler.open({
      name: 'Demo Site',
      description: 'Cube Game !!!!!!!!!!',
      // amount: amount * 100
    });
  }

  invokeStrip() {
    if(!window.document.getElementById('stripe-script')) {
      var s = window.document.createElement("script");
      s.id = "stripe-script";
      s.type = "text/javascript";
      s.src = "https://checkout.stripe.com/checkout.js";
      s.onload = () => {
        this.handler = (<any>window).StripeCheckout.configure({
          key: 'pk_test_51N96sVGXyzvXCyS2EriepHJroYOAvBjU6oml1UzutT6w69dpyNvihVqGwXjO4mWavAS4UQdCtwaafrK6VdbJWgao00Md3Ors8T',
          locale: 'auto',
          token: function (stripeToken: any) {
            console.log(stripeToken)
            alert('Payment Success!!');
          }
        });
      }

      window.document.body.appendChild(s);
    }
  }
  hidePayButton() {
    this.showPayButton = false;
  }

}
