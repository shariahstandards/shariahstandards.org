interface AuthConfig {
  clientID: string;
  domain: string;
  callbackURL: string;
  silentCallbackURL: string;
  audience: string;
  apiUrl: string;
}

export const AUTH_CONFIG: AuthConfig = {
  clientID: 'G6bKUq3LmpqoW2RXd6qK48jqfq7GbjDL',
  domain: 'shariahstandards.eu.auth0.com',
  callbackURL: 'http://localhost:4200',
  silentCallbackURL: 'http://localhost:4200/silent',
  audience: 'https://api.shariahstandards.org/',
  apiUrl: 'http://localhost:4500'
};