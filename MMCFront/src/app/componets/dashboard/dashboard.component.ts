import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router,ActivatedRoute } from '@angular/router';
import { UtilisateurDTO } from 'src/app/Models/utilisateurDTO';
import { ApiService } from 'src/app/services/api.service';
import { AuthService } from 'src/app/services/auth.service';
import { EtudiantStoreService } from 'src/app/services/user-store.service';
declare var window:any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  formModal :any

  public nom:string = "";

  etudiantInfo: any;

  public moduleId: any;
  
  demandes?: UtilisateurDTO[];

  userInfo: any;

  tousLesInfos: any

  utilisateurInfo: any;

  public Role!: string 



  constructor(private api :ApiService,private auth:AuthService, private etudiantStore:EtudiantStoreService
    ,private route:ActivatedRoute) {
      this.getName();
    }

  ngOnInit():void{

    this.getUserInfo();

    this.getToutesLesDemandes();

    this.etudiantStore.getRoleFromStore()
    .subscribe(val=>{
      const roleFromToken = this.auth.getEmailFromToken();
      this.Role = val || roleFromToken; 
    })


    this.formModal = new window.bootstrap.Modal(
      document.getElementById("exampleModal")
    );

    const token = localStorage.getItem('token');
  
    if (token) {
      this.api.getUSERSInfo(token).subscribe(
        (data) => {
          this.etudiantInfo = data;
        },
        (error) => {
          console.log(error);
        }
      );
    } else {
      console.log('Token not found');
    }

}

  openModal(){
    this.formModal.show();
  }

  closeModal(){
    this.formModal.hide();
  }

  getToutesLesDemandes(){
    this.api.getApprouverDemande().subscribe(demandes => {
      this.demandes = demandes;
    });
   }

  approuverDemande(demandeId: number){
    this.api.approuverDemande(demandeId)
      .subscribe(() => {
        this.getToutesLesDemandes();
      }),
      console.error();    
  }
  
  annulerDemande(dm:number){
    this.api.annullerDemande(dm)
    .subscribe(() =>{
      this.getToutesLesDemandes()
    })
  }

  getUserInfo() {
    this.api.getUserInfo().subscribe(
      (data) => {
        this.userInfo = data;
      },
      (error) => {
        console.error('Error fetching user info', error);
      }
    );
  }

  getName(){
    this.etudiantStore.getEmailFromStore()
    .subscribe(val =>{
      let nomFromToken = this.auth.getEmailFromToken();
      this.nom = val || nomFromToken
    })

  }
  
  Deconnexion(){
    this.auth.signOut()
  }


}
