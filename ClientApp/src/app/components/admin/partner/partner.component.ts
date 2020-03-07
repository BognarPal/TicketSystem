import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-partner',
  templateUrl: './partner.component.html',
  styleUrls: ['./partner.component.css']
})
export class PartnerComponent implements OnInit {

  users = [
    {
      id: 1, name: 'Béla'
    },
    {
      id:2, name: 'Ede'
    },
    {
      id: 3, name: 'Géza'
    },
    {
      id: 4, name: 'Pista'
    }
  ]

  selectedUsers = [];

  constructor() { }

  ngOnInit() {
  }

}
