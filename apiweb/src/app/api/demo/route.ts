import { NextResponse } from "next/server";
import prisma from "@/lib/prisma";

export async function GET() {
  const s = await prisma.freshersRegistered.deleteMany({});

  return NextResponse.json({
    s,
  });
}
