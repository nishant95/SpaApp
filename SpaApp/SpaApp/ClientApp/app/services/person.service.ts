import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { PersonDto } from '../dtos/person.dto';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/observable/from';

@Injectable()
export class PersonService {
    constructor(private http: Http) { }

    getPersons() {
        return this.http.get('http://localhost:49616/api/Person');
        //return Observable.from(persons);
    }
}

//const persons: PersonDto[] = [
//    {
//        FirstName: "Leonard",
//        LastName: "Gupta",
//        Dob: new Date(1993,4,12),
//        Email: "leonard.leo@gmail.com",
//        Id: 1,
//        MiddleName: "P",
//    },
//    {
//        FirstName: "Howard",
//        LastName: "Wolowitz",
//        Dob: new Date(1994, 5, 2),
//        Email: "howard.jew@gmail.com",
//        Id: 2,
//        MiddleName: "M",
//    },
//    {
//        FirstName: "Sheldon",
//        LastName: "Cooper",
//        Dob: new Date(1997, 2, 21),
//        Email: "cooper.sheldon@gmail.com",
//        Id: 3,
//        MiddleName: "K",
//    },
//    {
//        FirstName: "Rajesh",
//        LastName: "Koothrapalli",
//        Dob: new Date(1993, 12, 12),
//        Email: "indian.raj@gmail.com",
//        Id: 4,
//        MiddleName: "Ramayan",
//    }
//];
