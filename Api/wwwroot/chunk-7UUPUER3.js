import{b as c,ma as l}from"./chunk-66ZLYJGL.js";import{ea as d,ja as f,ra as o}from"./chunk-KOJY6QOY.js";var $=class t{constructor(e){this.http=e}baseApiUrl=l.apiUrl;orderCompleted=!1;createOrder(e){return this.http.post(`${this.baseApiUrl}orders`,e)}getOrdersForUser(){return this.http.get(`${this.baseApiUrl}orders`)}getOrderDetailed(e){return this.http.get(`${this.baseApiUrl}orders/${e}`)}static \u0275fac=function(n){return new(n||t)(f(c))};static \u0275prov=d({token:t,factory:t.\u0275fac,providedIn:"root"})};var y=class t{transform(e,...n){if(e&&"address"in e&&e.name){let{line1:r,line2:i,city:s,state:a,country:p,postal_code:m}=e?.address;return`${e.name}, ${r}${i?", "+i:""},${s}, ${a}, ${m}, ${p}`}else if(e&&"line1"in e){let{line1:r,line2:i,city:s,state:a,country:p,postalCode:m}=e;return`${e.name}, ${r}${i?", "+i:""},${s}, ${a}, ${m}, ${p}`}else return"Unknown Address"}static \u0275fac=function(n){return new(n||t)};static \u0275pipe=o({name:"address",type:t,pure:!0,standalone:!0})};var h=class t{transform(e,...n){if(e&&"card"in e){let r=e.card;return r?`
                    ${r?.brand.toLocaleUpperCase()} ${r.funding.toLocaleUpperCase()}, 
                    **** **** **** ${r.last4}, 
                    Exp.: ${r.exp_month}/${r.exp_year} - 
                    ${r.country}
                    `:"Unknown payment details"}else if(e&&"last4"in e){let r=e;return r?`${r?.brand.toLocaleUpperCase()}, **** **** **** ${r.last4}, Exp.: ${r.expMonth}/${r.expYear}`:"Unknown payment details"}else return"Unknown Payment Method"}static \u0275fac=function(n){return new(n||t)};static \u0275pipe=o({name:"paymentDetails",type:t,pure:!0,standalone:!0})};export{$ as a,y as b,h as c};
