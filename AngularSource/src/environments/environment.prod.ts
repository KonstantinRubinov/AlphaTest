export const environment = {
  production: true
};

export const mainUrl="http://localhost:61081/";
export const loginUrl=mainUrl+"token";
export const usersUrl=mainUrl+"api/users/";

export const checkUser=usersUrl+"check/";


export const isFollowedByMe=usersUrl+"follower/";
export const follow=usersUrl+"follow/";
export const unfollow=usersUrl+"unfollow/";

export const fortable=usersUrl+"fortable/";