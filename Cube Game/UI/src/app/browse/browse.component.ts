import { Component, OnInit } from '@angular/core';
import { ProductBrowseService } from 'src/Services/product-browse.service';

import { SearchService } from 'src/Services/shared.service';
import { Product } from 'src/Services/wishlist.service';

@Component({
  selector: 'app-browse',
  templateUrl: './browse.component.html',
  styleUrls: ['./browse.component.css']
})
export class BrowseComponent implements OnInit {
  searchResults: any[] = [];
  AllProduct : any

  categoryname :any
  price :any
  platform :any
  developerName :any

  filteredProducts: any[] = [];
  constructor(public myService : ProductBrowseService,private searchService: SearchService){}

  filtercategory(event:any)
  {
    let value = event.target.value;
    this.getproductscategory(value);
  }
  getproductscategory(x:any)
  {
    this.myService.GetProductByCategoryID(x).subscribe((data:any)=>{
      this.filteredProducts = data;
    })
  }

  filterprice(event:any)
  {
    let value = event.target.value;
    this.getproductsprice(value);
  }
  getproductsprice(x:any)
  {
    this.myService.GetProductByPriceID(x).subscribe((data:any)=>{
      this.filteredProducts = data;
    })
  }

  filterdev(event:any)
  {
    let value = event.target.value;
    this.getproductsdev(value);
  }
  getproductsdev(x:any)
  {
    this.myService.GetProductByDevID(x).subscribe((data:any)=>{
      this.filteredProducts = data;
    })
  }

  ngOnInit(): void {
    this.myService.GetAllProduct().subscribe({
      next: (data) => {
        this.searchService.setAllProducts(data);
        this.filteredProducts = [...this.AllProduct]; // Initialize filteredProducts with all products
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.searchService.searchResults$.subscribe((results: any[]) => {
      this.searchResults = results;
      this.filteredProducts = [...results]; // Update filteredProducts with search results
    });

    this.myService.GetAllcategoryname().subscribe({
      next:(data)=>{
        this.categoryname = data;
      },
      error:(err)=>{
        console.log(err);
      }
    });

    this.myService.GetAllProductPrice().subscribe({
      next:(data)=>{
        this.price = data;
      },
      error:(err)=>{
        console.log(err);
      }
    });

    this.myService.GetAllProductDev().subscribe({
      next:(data)=>{
        this.developerName = data;
      },
      error:(err)=>{
        console.log(err);
      }
    });
  }
}
