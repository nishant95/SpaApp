import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { PersonService } from '../../../services';
import { SecurityService } from '../../../services';
import { PersonDto } from '../../../dtos';

@Component({
    selector: 'person',
    template: require('./add-person.component.html')
})
export class AddPersonComponent {
    constructor(private router: Router,
        private personService: PersonService,
        private securityService: SecurityService) { }

    person: PersonDto = new PersonDto();

    addPerson() {
        this.personService.addPerson(this.person).subscribe(
            value => {
                this.router.navigateByUrl('person/view');
            },
            error => {
                this.router.navigateByUrl('person/view');
            });
    }
}
