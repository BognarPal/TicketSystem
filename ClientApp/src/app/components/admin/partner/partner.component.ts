import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../../services/admin.service';

@Component({
  selector: 'app-partner',
  templateUrl: './partner.component.html'
})
export class PartnerComponent implements OnInit {

  users = []
  partners = []
  errorText = '';
  selectedPartner = null;
  editMode = false;

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.getList('partners').subscribe(
      (data: any[]) => {
        this.partners = data;
        this.selectedPartner = this.partners.length > 0 ? this.partners[0] : null;
        if (this.selectedPartner) {
          this.selectedPartner.selected = true;
        }
      },
      err => this.errorText = err.error || err.message
    );

    this.adminService.getList('usernames').subscribe(
      (data: any[]) => {
        this.users = data;
      },
      err => this.errorText = err.error || err.message
    );
  }

  row_click(partner: any) {
    if (!partner.selected && !this.editMode) {
      this.partners.forEach(p => p.selected = false);
      partner.selected = true;
      this.selectedPartner = partner;
    }
  }

  modify() {
    this.editMode = true;
    this.adminService.getPartner(this.selectedPartner.id).subscribe(
      (data) => {
        const index = this.partners.findIndex(p => p.id === this.selectedPartner.id);
        this.partners[index] = data;
        this.partners[index].selected = true;
        this.selectedPartner = JSON.parse(JSON.stringify(data));
      },
      err => {
        this.errorText = err.error || err.message;
      }
    );
  }

  new() {
    this.editMode = true;
    this.selectedPartner = JSON.parse('{}');
    this.selectedPartner.users = [];
  }

  delete() {
    this.errorText = '';    
    if (this.selectedPartner.id) {
      // TODO: megerősítő kérdés!!
      this.adminService.savePartner('deletepartner', this.selectedPartner).subscribe(
        (data:boolean) => {
          const index = this.partners.findIndex(p => p.id === this.selectedPartner.id);
          this.partners.splice(index, 1);
          this.selectedPartner = this.partners.length > 0 ? this.partners[0] : null;
          if (this.selectedPartner) {
            this.selectedPartner.selected = true;
          }
        },
        err => {
          this.errorText = err.error || err.message;
        }
      );
    }
  }

  cancel() {
    this.editMode = false;
    this.selectedPartner = this.partners.filter(p => p.selected)[0];
  }

  save() {
    this.errorText = '';
    this.selectedPartner.active = this.selectedPartner.active === 'true';
    if (this.selectedPartner.id) {  //azaz módosítás
      this.adminService.savePartner('updatepartner', this.selectedPartner).subscribe(
        data => {
          const index = this.partners.findIndex(p => p.id === this.selectedPartner.id);
          this.partners[index] = data;
          this.partners[index].selected = true;
          this.selectedPartner = this.partners[index];
          this.editMode = false;
        },
        err => {
          this.errorText = err.error || err.message;
        }
      );
    } else {                       //azaz új rögzítés
      this.adminService.savePartner('insertpartner', this.selectedPartner).subscribe(
        data => {
          this.partners.push(data);
          this.partners.forEach(p => p.selected = false);
          this.selectedPartner = data;
          data.selected = true;
          this.editMode = false;
        },
        err => {
          this.errorText = err.error || err.message;
        });
    }
  }

}
