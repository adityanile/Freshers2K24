import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";

export async function GET() {
  const s = await prisma.freshersRegistered.findMany({});
  return NextResponse.json({
    status: "success",
    msg: "List of Registered Students",
    s,
  });
}
