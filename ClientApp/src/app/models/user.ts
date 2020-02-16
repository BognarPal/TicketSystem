export class User {
  id: string;
  username: string;
  // password: string;
  roles: string[];
  token?: string;
  lastAccess: Date;
  validTo: Date;

  loadFromLocalStorage() {
    const usr = JSON.parse(localStorage.getItem('currentUser'));
    if (usr) {
      this.id = usr.id;
      this.username = usr.username;
      this.roles = usr.roles;
      this.token = usr.token;
      this.lastAccess = new Date(usr.lastAccess);
      this.validTo = new Date(usr.validTo);
    }
  }
}
