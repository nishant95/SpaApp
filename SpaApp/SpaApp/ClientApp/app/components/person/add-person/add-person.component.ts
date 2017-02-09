import { Component } from '@angular/core';
import { PersonService } from '../../../services/person.service';
import { PersonDto } from '../../../dtos/person.dto';

@Component({
    selector: 'person',
    template: require('./add-person.component.html')
})
export class AddPersonComponent {
    constructor(private personService: PersonService) { }

    persons: PersonDto;

    addPerson() {

    }
}
