import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProductBrowseService {

  constructor(private myClient : HttpClient) { }

  private Base_URL = "https://localhost:7121/api/Product"
  private imgs_URL = "https://localhost:7121/api/Product/ImagesProduct"
  private URL = "https://localhost:7121/api/Category"
  private Base_URLL ="https://localhost:7121/api/Product/GetProductsByCategory"
  private URL_price = "https://localhost:7121/api/Product/GetProductsByPrice"
  private URL_platform = "https://localhost:7121/api/Product/GetProductsByPlatform"
  private URL_Dev = "https://localhost:7121/api/Product/GetProductsByDeveloperName"


  GetAllProduct(){

    return this.myClient.get(this.Base_URL)

  }
  GetAllProductPrice(){

    return this.myClient.get(this.Base_URL)

  }
  GetAllProductPlatform(){

    return this.myClient.get(this.Base_URL)

  }
  GetAllProductDev(){

    return this.myClient.get(this.Base_URL)

  }
  GetProductByID(id:any){
    return this.myClient.get(`${this.Base_URL}/${id}`);
  }

  GetProductImageByID(id:any){

    return this.myClient.get(`${this.imgs_URL}?Productid=${id}`);

  }

  GetAllcategoryname()
  {
    return this.myClient.get(this.URL);
  }

  GetProductByCategoryID(id:any)
  {
    return this.myClient.get(`${this.Base_URLL}/${id}`);
  }

  GetProductByPriceID(id:any)
  {
    return this.myClient.get(`${this.URL_price}/${id}`);
  }

  GetProductByPlatformID(platform:any)
  {
    return this.myClient.get(`${this.URL_platform}/${platform}`);
  }
  GetProductByDevID(developerName:any)
  {
    return this.myClient.get(`${this.URL_Dev}/${developerName}`);
  }


}
