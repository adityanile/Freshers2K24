import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";

export async function POST(req: Request) {
  const { prn, name } = await req.json();

  if (!prn || !name)
    return NextResponse.json({ status: "fail", msg: "Invalid Params" });

  const std = await prisma.freshersRegistered.findFirst({
    where: {
      prn: prn,
    },
  });

  if (std)
    return NextResponse.json({ status: "fail", msg: "Already Registered" });

  const newS = await prisma.freshersRegistered.create({
    data: {
      name: name,
      prn: prn,
    },
  });
  return NextResponse.json({
    status: "success",
    msg: "Registration Successful",
    newS,
  });
}
