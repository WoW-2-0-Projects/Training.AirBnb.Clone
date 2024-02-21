# Identity infrastructure of Air Bnb

## Features content

- [User sign up](#user-creation)


### Sign up

Frontend :

<img style="width: 100%" src="assets/2.png" alt="granting/revoking role process diagram"/>

- validate sign up details
- send sign up request

Backend : 

<img style="width: 100%" src="assets/1.png" alt="Entity relationship diagram"/>

- check user existence
- create user with default roles
- publish user created event

#### Granting / revoking role

<img style="width: 100%" src="assets/3.png" alt="granting/revoking role process diagram"/>

<!-- TODO : Add admin grand / revoke role frontend logic -->

- validate permission
- check user and role existence
- create / delete user role

### Sign-in

Frontend :

<img style="width: 100%" src="assets/5.png" alt="granting/revoking role process diagram"/>

- validate sign in details
- send sign in request
- store access and refresh tokens and account

Backend : 

<img style="width: 100%" src="assets/6.png" alt="granting/revoking role process diagram"/>

- validate user existence
- generate access and refresh token

#### Sign-out

Frontend : 

<img style="width: 100%" src="assets/7.png" alt="granting/revoking role process diagram"/>

- send sign out request
- remove access and refresh token along with account

Backend :

<img style="width: 100%" src="assets/8.png" alt="granting/revoking role process diagram"/>

- validate access token
- remove access and refresh token

#### Access token generation

#### Refresh access token

#### User verification

#### Account lock and unlock

#### Password reset, Password change, Password expiration and Password rotate

#### Entities relationship

