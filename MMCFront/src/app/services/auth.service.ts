import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http"
import { Router } from '@angular/router';
import {JwtHelperService} from '@auth0/angular-jwt'

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  private baseUrl:string = "https://localhost:44323/api/Administration/"

  private EtudiantPayload = this.decodeToken();

  constructor(private http : HttpClient,private router:Router) {}


  login(loginObj: any){
    return this.http.post<any>(`${this.baseUrl}login`,loginObj)
  }
  
  signUp(etudiantObj:any){
    return this.http.post<any>(`${this.baseUrl}AddUser`,etudiantObj)
  }

  signOut(){
     localStorage.clear();
     this.router.navigate(['login']);
  }

  storeToken(tokenValue:string){
    localStorage.setItem('token',tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn():boolean{
    return !!localStorage.getItem('token')
  }

  decodeToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken()!;
    console.log(jwtHelper.decodeToken(token))
    return jwtHelper.decodeToken(token)
  }

  getEmailFromToken(){
    if(this.EtudiantPayload){
      return this.EtudiantPayload.unique_name;
    }
  }


}
