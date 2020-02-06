export class User {
  email: string;
  token?: string;
  expiration: Date;
  username: string;
  roles: string[];
}
