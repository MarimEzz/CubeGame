import { Component, OnInit} from '@angular/core';
import { MostPlayedService } from '../../Services/most-played.service';
import { AuthService } from 'src/Services/auth.service';
import { CartService } from 'src/Services/cart.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-main-slider',
  templateUrl: './main-slider.component.html',
  styleUrls: ['./main-slider.component.css']
})
export class MainSliderComponent implements OnInit {
  constructor(public myserv:MostPlayedService ,
     private auth : AuthService ,
     private myservice : CartService , private route : Router ){}


  user:any;
  currentIndex: number = 0;
  currentImage: any;

  startInterval() {
    setInterval(() => {
      this.currentIndex = (this.currentIndex + 1) % this.user.length;
      this.currentImage = this.user[this.currentIndex];
    }, 3000);
  }

  changeImage(image: string, index: number) {
    this.currentImage = image;
    this.currentIndex = index;
  }

  ngOnInit(): void {
    this.startInterval();
    this.myserv.GetSomeMostPlayed().subscribe({
      next:(data)=>{
        this.user=data;
      },
      error:(err)=>{console.log(err)}
    })
  }

  AddToCart(id : any) {
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
}
