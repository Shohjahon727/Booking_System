export const initialHotels = [
  {
    id: 1,
    name: "Grand Plaza Hotel",
    city: "Toshkent",
    address: "Toshkent, Mirobod tumani, Amir Temur shox ko'chasi 45",
    rating: 4.8,
    reviewsCount: 120,
    pricePerNight: 750000,
    featuredPriceText: "dan 350 000 so'm/kun",
    starCount: 5,
    verified: true,
    image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=800&q=80",
    gallery: [
      "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=800&q=80",
      "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=600&q=80",
      "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=600&q=80",
      "https://images.unsplash.com/photo-1578683010236-d716f9a3f461?auto=format&fit=crop&w=600&q=80",
      "https://images.unsplash.com/photo-1618773928121-c32242e63f39?auto=format&fit=crop&w=600&q=80"
    ],
    amenities: [
      { name: "Wi-Fi", icon: "wifi" },
      { name: "Basseyn", icon: "waves" },
      { name: "Parking", icon: "car" },
      { name: "Restoran", icon: "utensils" },
      { name: "Spa", icon: "sparkles" },
      { name: "Fitness", icon: "dumbbell" }
    ],
    description: "Zamonaviy va barcha qulayliklarga ega premium darajadagi mehmonxona. Shahar markazida, asosiy diqqatga sazovor maskanlar yaqinida joylashgan. Mehmonlarimiz uchun bepul Wi-Fi, ochiq basseyn, 24/7 restoran va konferentsiya zallari xizmat ko'rsatadi.",
    roomsCount: 45,
    status: "Active",
    isFavorite: true
  },
  {
    id: 2,
    name: "City Palace Hotel",
    city: "Toshkent",
    address: "Toshkent, Markaz, Navoiy ko'chasi 12",
    rating: 4.6,
    reviewsCount: 95,
    pricePerNight: 650000,
    featuredPriceText: "dan 320 000 so'm/kun",
    starCount: 4,
    verified: true,
    image: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?auto=format&fit=crop&w=800&q=80",
    gallery: [
      "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?auto=format&fit=crop&w=800&q=80",
      "https://images.unsplash.com/photo-1591088398332-8a7791972843?auto=format&fit=crop&w=600&q=80",
      "https://images.unsplash.com/photo-1595576508898-0ad5c879a061?auto=format&fit=crop&w=600&q=80"
    ],
    amenities: [
      { name: "Wi-Fi", icon: "wifi" },
      { name: "Parking", icon: "car" },
      { name: "Restoran", icon: "utensils" },
      { name: "Fitness", icon: "dumbbell" }
    ],
    description: "Biznes sayohatchilar va oilaviy dam oluvchilar uchun ideal sharoitlar. Yuqori tezlikdagi internet va shinam xonalar.",
    roomsCount: 32,
    status: "Active",
    isFavorite: false
  },
  {
    id: 3,
    name: "Comfort Room",
    city: "Samarqand",
    address: "Samarqand, Registon yaqinida, Dahbed ko'chasi 8",
    rating: 4.7,
    reviewsCount: 110,
    pricePerNight: 550000,
    featuredPriceText: "dan 280 000 so'm/kun",
    starCount: 4,
    verified: true,
    image: "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?auto=format&fit=crop&w=800&q=80",
    gallery: [
      "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?auto=format&fit=crop&w=800&q=80",
      "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2?auto=format&fit=crop&w=600&q=80"
    ],
    amenities: [
      { name: "Wi-Fi", icon: "wifi" },
      { name: "Nonushta", icon: "coffee" },
      { name: "Konditsioner", icon: "wind" }
    ],
    description: "Samarqandning tarixiy markazida joylashgan qulay va osoyishta mehmonxona. Samarqandning qadimiy obidalari piyoda masofada.",
    roomsCount: 20,
    status: "Active",
    isFavorite: true
  },
  {
    id: 4,
    name: "Registon Hotel",
    city: "Samarqand",
    address: "Samarqand, Registon maydoni ro'parasi",
    rating: 4.9,
    reviewsCount: 210,
    pricePerNight: 850000,
    featuredPriceText: "dan 400 000 so'm/kun",
    starCount: 5,
    verified: true,
    image: "https://images.unsplash.com/photo-1571896349842-33c89424de2d?auto=format&fit=crop&w=800&q=80",
    gallery: [
      "https://images.unsplash.com/photo-1571896349842-33c89424de2d?auto=format&fit=crop&w=800&q=80",
      "https://images.unsplash.com/photo-1584132967334-10e028bd69f7?auto=format&fit=crop&w=600&q=80"
    ],
    amenities: [
      { name: "Wi-Fi", icon: "wifi" },
      { name: "Basseyn", icon: "waves" },
      { name: "Restoran", icon: "utensils" },
      { name: "Spa", icon: "sparkles" },
      { name: "Terrasa", icon: "sun" }
    ],
    description: "Sharqona me'morchilik va zamonaviy Yevropa qulayliklarini o'zida uyg'unlashtirgan 5 yulduzli lyuks mehmonxona.",
    roomsCount: 38,
    status: "Active",
    isFavorite: false
  }
];

export const initialRooms = [
  {
    id: 101,
    hotelId: 1,
    name: "Deluxe Room",
    type: "Deluxe",
    capacity: 2,
    size: "35 m²",
    breakfastIncluded: true,
    pricePerNight: 910000,
    beds: "1 katta ikki kishilik karavot (King)",
    floor: "1-Qavat / 101",
    status: "Active",
    image: "https://images.unsplash.com/photo-1590490360182-c33d57733427?auto=format&fit=crop&w=500&q=80"
  },
  {
    id: 102,
    hotelId: 1,
    name: "Suite Room",
    type: "Suite",
    capacity: 3,
    size: "50 m²",
    breakfastIncluded: true,
    pricePerNight: 1350000,
    beds: "1 King karavot + 1 Divan-karavot",
    floor: "2-Qavat / 202",
    status: "Active",
    image: "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?auto=format&fit=crop&w=500&q=80"
  },
  {
    id: 103,
    hotelId: 1,
    name: "Standard Room",
    type: "Standard",
    capacity: 2,
    size: "25 m²",
    breakfastIncluded: true,
    pricePerNight: 600000,
    beds: "2 ta bir kishilik karavot",
    floor: "3-Qavat / 303",
    status: "Active",
    image: "https://images.unsplash.com/photo-1618773928121-c32242e63f39?auto=format&fit=crop&w=500&q=80"
  },
  {
    id: 104,
    hotelId: 1,
    name: "Deluxe Twin Room",
    type: "Deluxe Twin",
    capacity: 2,
    size: "32 m²",
    breakfastIncluded: false,
    pricePerNight: 850000,
    beds: "2 ta qulay alohida karavot",
    floor: "4-Qavat / 401",
    status: "Active",
    image: "https://images.unsplash.com/photo-1578683010236-d716f9a3f461?auto=format&fit=crop&w=500&q=80"
  }
];

export const initialBookings = [
  {
    id: "BRON-8492",
    hotelName: "Grand Plaza Hotel",
    guestName: "Alisher Normatov",
    guestPhone: "+998 90 123 45 67",
    guestEmail: "alisher@mail.com",
    roomName: "Deluxe Room",
    roomType: "Deluxe",
    checkIn: "2025-05-15",
    checkOut: "2025-05-18",
    nights: 3,
    guests: 2,
    totalPrice: 1200000,
    status: "Confirmed",
    services: ["Nonushta", "Transfer"],
    image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=300&q=80"
  },
  {
    id: "BRON-7731",
    hotelName: "City Palace Hotel",
    guestName: "Diyorbek Toshmatov",
    guestPhone: "+998 93 555 12 34",
    guestEmail: "diyorbek@mail.com",
    roomName: "Suite Room",
    roomType: "Suite",
    checkIn: "2025-05-20",
    checkOut: "2025-05-22",
    nights: 2,
    guests: 2,
    totalPrice: 1800000,
    status: "Pending",
    services: ["Nonushta"],
    image: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?auto=format&fit=crop&w=300&q=80"
  },
  {
    id: "BRON-6520",
    hotelName: "Comfort Room",
    guestName: "Sardor Umarov",
    guestPhone: "+998 97 789 00 11",
    guestEmail: "sardor@mail.com",
    roomName: "Standard Room",
    roomType: "Standard",
    checkIn: "2025-04-10",
    checkOut: "2025-04-12",
    nights: 2,
    guests: 1,
    totalPrice: 750000,
    status: "Completed",
    services: [],
    image: "https://images.unsplash.com/photo-1551882547-ff40c63fe5fa?auto=format&fit=crop&w=300&q=80"
  },
  {
    id: "BRON-5114",
    hotelName: "Registon Hotel",
    guestName: "John Smith",
    guestPhone: "+1 202 555 0199",
    guestEmail: "john.smith@domain.com",
    roomName: "Suite Room",
    roomType: "Suite",
    checkIn: "2025-05-01",
    checkOut: "2025-05-04",
    nights: 3,
    guests: 2,
    totalPrice: 1100000,
    status: "Cancelled",
    services: ["Transfer", "Spa"],
    image: "https://images.unsplash.com/photo-1571896349842-33c89424de2d?auto=format&fit=crop&w=300&q=80"
  },
  {
    id: "BRON-9902",
    hotelName: "Grand Plaza Hotel",
    guestName: "Arthur Vane",
    guestPhone: "+44 7911 123456",
    guestEmail: "arthur@vane.uk",
    roomName: "Deluxe Twin",
    roomType: "Deluxe",
    checkIn: "2025-05-28",
    checkOut: "2025-05-30",
    nights: 2,
    guests: 2,
    totalPrice: 1450000,
    status: "Confirmed",
    services: ["Nonushta"],
    image: "https://images.unsplash.com/photo-1566073771259-6a8506099945?auto=format&fit=crop&w=300&q=80"
  }
];

export const initialCalendar = {
  month: "May 2025",
  days: [
    { name: "Dushanba", dayNum: 25 },
    { name: "Seshanba", dayNum: 26 },
    { name: "Chorshanba", dayNum: 27 },
    { name: "Payshanba", dayNum: 28 },
    { name: "Juma", dayNum: 29 },
    { name: "Shanba", dayNum: 30 },
    { name: "Yakshanba", dayNum: 31 }
  ],
  rooms: [
    {
      id: 1,
      name: "1-Xona (Deluxe 101)",
      bookings: [
        { startDay: 26, duration: 3, guest: "Alisher N. (Band)", status: "booked" }
      ]
    },
    {
      id: 2,
      name: "2-Xona (Suite 201)",
      bookings: [
        { startDay: 25, duration: 2, guest: "Diyorbek S.", status: "booked" },
        { startDay: 29, duration: 2, guest: "John Smith", status: "booked" }
      ]
    },
    {
      id: 3,
      name: "3-Xona (Standard 301)",
      bookings: [
        { startDay: 27, duration: 2, guest: "Bobo Mirzo", status: "pending" }
      ]
    },
    {
      id: 4,
      name: "4-Xona (Twin 401)",
      bookings: [
        { startDay: 28, duration: 3, guest: "Malika K. (Band)", status: "booked" }
      ]
    },
    {
      id: 5,
      name: "5-Xona (Executive 501)",
      bookings: [
        { startDay: 29, duration: 2, guest: "Arthur Vane", status: "booked" }
      ]
    }
  ]
};

export const initialReviews = [
  {
    id: 1,
    author: "Alisher Normatov",
    avatarText: "AN",
    date: "15.05.2025",
    rating: 5,
    comment: "Xizmat ko'rsatish a'lo darajada. Xonalar toza va shinam, xodimlar juda xushmuomala. Keyingi safarimda ham albatta shu yerni tanlayman!"
  },
  {
    id: 2,
    author: "Diyorbek Tozagov",
    avatarText: "DT",
    date: "10.05.2025",
    rating: 5,
    comment: "Ajoyib joy, nonushta juda mazali va xilma-xil. Shahar markazida bo'lgani sayohat qilish uchun juda qulay."
  },
  {
    id: 3,
    author: "John Smith",
    avatarText: "JS",
    date: "05.05.2025",
    rating: 4,
    comment: "Great location and very helpful staff! Room was spacious and bed was comfortable. Recommended for business trips."
  }
];

export const initialRatingsBreakdown = [
  { star: 5, count: 80, percent: 66 },
  { star: 4, count: 25, percent: 21 },
  { star: 3, count: 10, percent: 8 },
  { star: 2, count: 3, percent: 3 },
  { star: 1, count: 2, percent: 2 }
];

export const initialUsers = [
  {
    id: 1,
    name: "Alisher Normatov",
    email: "alisher@mail.com",
    role: "Admin",
    phone: "+998 90 123 45 67",
    status: "Active",
    registeredDate: "2024-01-15"
  },
  {
    id: 2,
    name: "Grand Hotel Egasi",
    email: "owner.grand@mail.com",
    role: "Hotel",
    phone: "+998 93 456 78 90",
    status: "Active",
    registeredDate: "2024-02-10"
  },
  {
    id: 3,
    name: "Diyorbek Sodiqov",
    email: "diyorbek@mail.com",
    role: "User",
    phone: "+998 91 234 56 78",
    status: "Active",
    registeredDate: "2024-04-05"
  },
  {
    id: 4,
    name: "City Palace Menejeri",
    email: "citypalace@mail.com",
    role: "Hotel",
    phone: "+998 94 333 22 11",
    status: "Active",
    registeredDate: "2024-03-12"
  },
  {
    id: 5,
    name: "Malika Karimova",
    email: "malika.k@mail.com",
    role: "User",
    phone: "+998 99 888 77 66",
    status: "Active",
    registeredDate: "2024-05-18"
  }
];

export const initialMessages = [
  {
    id: 1,
    from: "Grand Plaza Support",
    time: "Bugun 10:24",
    text: "Hurmatli Alisher, sizning broningiz muvaffaqiyatli tasdiqlandi. Aeroportdan transfer buyurtma qilishni xohlaysizmi?",
    unread: true,
    avatar: "GP"
  },
  {
    id: 2,
    from: "Tizim Administratori",
    time: "Kecha 18:30",
    text: "Xavfsizlik eslatmasi: Hisobingizga yangi qurilmadan kirish amalga oshirildi.",
    unread: false,
    avatar: "TA"
  },
  {
    id: 3,
    from: "City Palace Hotel",
    time: "14 May 12:00",
    text: "Sizga maxsus 15% chegirmali vaucher taqdim etildi! Foydalanish kodi: CITY2025",
    unread: false,
    avatar: "CP"
  }
];
