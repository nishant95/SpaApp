import { Inject,Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { PersonDto } from '../dtos/person.dto';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/from';
import { APP_CONFIG, IAppConfig } from '../app-config';
import { SecurityService } from './security.service';

@Injectable()
export class PersonService {
    constructor( @Inject(APP_CONFIG) private config: IAppConfig,
        private http: Http,
        private securityService: SecurityService) {
        this.actionUrl = `${config.apiEndpoint}Person/`;
    }

    private actionUrl: string;
    private headers: Headers;

    private setHeaders() {

        console.log('personservice: setHeaders started');

        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
        this.headers.append('Accept', 'application/json');

        let token = this.securityService.GetToken();
        if (token !== '') {
            let tokenValue = 'Bearer ' + token;
            console.log('tokenValue:' + tokenValue);
            this.headers.append('Authorization', tokenValue);
        }
    }

    getPersons() {
        this.setHeaders();
        let options = new RequestOptions({ headers: this.headers, body: '' });
        return this.http.get(this.actionUrl, options);
        //return Observable.from(persons); //Dummy data for UI test
    }

    addPerson(person: PersonDto) {
        this.setHeaders();
        let options = new RequestOptions({ headers: this.headers });
        return this.http.post(this.actionUrl, person, options);
    }
}

//Dummy data for UI test
const persons: PersonDto[] = [
    {
        FirstName: "Leonard",
        LastName: "Gupta",
        Dob: new Date(1993,4,12),
        Email: "leonard.leo@gmail.com",
        Id: 1,
        MiddleName: "P",
    },
    {
        FirstName: "Howard",
        LastName: "Wolowitz",
        Dob: new Date(1994, 5, 2),
        Email: "howard.jew@gmail.com",
        Id: 2,
        MiddleName: "M",
    },
    {
        FirstName: "Sheldon",
        LastName: "Cooper",
        Dob: new Date(1997, 2, 21),
        Email: "cooper.sheldon@gmail.com",
        Id: 3,
        MiddleName: "K",
    },
    {
        FirstName: "Rajesh",
        LastName: "Koothrapalli",
        Dob: new Date(1993, 12, 12),
        Email: "indian.raj@gmail.com",
        Id: 4,
        MiddleName: "Ramayan",
    }
];
