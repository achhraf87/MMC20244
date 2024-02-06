import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UtilisateurDTO } from '../Models/utilisateurDTO';




@Injectable({
  providedIn: 'root'
})
export class ApiService {

  //public programmeId: any;

  private  APlIurl: string = 'https://localhost:7210/api/';

  private Etudiant: string = this.APlIurl+'Etudiant';

  private ProMod: string = this.APlIurl+'Etudiant';

  private Note : string = this.APlIurl+'Etudiant';

  private Profile : string = this.APlIurl+'Etudiant';

  private ToutesLesnotes : string = this.APlIurl+'Etudiant';

  private ProgrammeAssoci: string = this.APlIurl+'Etudiant';

  private APIurl: string = 'https://localhost:44323/api/';



  private ToutesLesDemandes = this.APIurl+'Administration';

  private InfoUser = this.APIurl+'Administration/info';


  constructor(private http: HttpClient) { }

  GetEtudiant(){
    return this.http.get<any>(`${this.Etudiant}/Spekear`)
  }

  getEtudiantInfo(token: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<any>(`${this.Profile}/info`, { headers });

  }

  getApprouverDemande():Observable<UtilisateurDTO[]> {
    return this.http.get<UtilisateurDTO[]>(`${this.ToutesLesDemandes}/getToutesLesDemandes`);
  }

   approuverDemande(demandeId: number): Observable<any> {
    const url = `${this.ToutesLesDemandes}/approuver-demande/${demandeId}`;
    return this.http.post(url, null);
  }

  annullerDemande(demandeId :number) :Observable<any>{
    const url = `${this.ToutesLesDemandes}/Annuler-Demande/${demandeId}`;
    return this.http.post(url, null); 
  }

  getUserInfo(): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });

    return this.http.get<any>(`${this.ToutesLesDemandes}/info`, { headers });
  }

  getUserInfos(): Observable<any> {
    const token = localStorage.getItem('token');
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
      })
    };
    return this.http.get<any>(`${this.ToutesLesDemandes}/GetInfoUsers`, httpOptions);
  }

  getUSERSInfo(token: string) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });

    return this.http.get<any>(`${this.Profile}/info`, { headers });

  }




}








