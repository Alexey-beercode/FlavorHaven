export const environment = {
  production: false,
  baseApiUrl: 'http://localhost:8081',
  apiUrls: {
    auth: {
      login: '/api/auth/login',
      register: '/api/auth/register',
      refreshToken: '/api/auth/refresh',
      revoke: '/api/auth/revoke',
    },
    cart: {
      addByUserId: '/api/cart/add-by-user-id',
      removeByUserId: '/api/cart/remove-by-user-id',
      clearByUserId: '/api/cart/clear-by-user-id',
      getByUserId: '/api/cart/get-by-user-id',
    },
    dishCategory: {
      getAll: '/api/dish-category/get-all',
      getById: '/api/dish-category/get-by-id',
    },
    dish: {
      getById: '/api/dish/get-by-id',
      getByParameters: '/api/dish/get-by-parameters',
    },
    order: {
      createByUserId: '/api/order/create-by-user-id',
      getById: '/api/order/get-by-id',
      getByStatus: '/api/order/get-by-status',
      getByUser: '/api/order/get-by-user',
    },
    orderStatus: {
      getAll: '/api/order-status/get-all',
      getById: '/api/order-status/get-by-id',
    },
    payment: {
      create: '/api/payment/create',
      getAll: '/api/payment/get-all',
      getById: '/api/payment/get-by-id',
      getByOrder: '/api/payment/get-by-order',
      getByUser: '/api/payment/get-by-user',
    },
    review: {
      create: '/api/review/create',
      delete: '/api/review/delete',
      getById: '/api/review/get-by-id',
      getByOrder: '/api/review/get-by-order',
      getByUser: '/api/review/get-by-user',
    },
    role: {
      getAll: '/api/role/get-all',
      getById: '/api/role/get-by-id',
      getByName: '/api/role/get-by-name',
      getByUser: '/api/role/get-by-user',
    },
    user: {
      getAll: '/api/user/get-all',
      getById: '/api/user/get-by-id',
      updateBalance: '/api/user/update-balance',
    },
  }
};
