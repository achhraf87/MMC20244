import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EtudiantStoreService {

  private unique_name = new BehaviorSubject<string>("");
  private role$ = new BehaviorSubject<string>("");

  constructor() { }

  public getRoleFromStore(){
    return this.role$.asObservable();
  }

  public setRoleFromStore(role:string){
     this.role$.next(role)
  }

  public getEmailFromStore(){
    return this.unique_name.asObservable();
  }

  public setEmailFromStore(unique_name:string){
    this.unique_name.next(unique_name)
  }

}
